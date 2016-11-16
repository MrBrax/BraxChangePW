using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;

namespace BraxChangePW {

	public partial class ChangePWForm : Form {

		public string sUsername;
		public string sPassword;
		public string sMessage;
		public string sPhone;
		public bool ValidUser = false;

		PrincipalContext adContext;
		UserPrincipal adUser;

		public ChangePWForm() {
			InitializeComponent();

			try {
				adContext = new PrincipalContext(ContextType.Domain);
			} catch (Exception ex) {
				SystemSounds.Asterisk.Play();
				MessageBox.Show("AD DS Error: " + ex.Message);
				statusLabel.Text = "AD DS Error: " + ex.Message;
			}

			inputMessageTemplate.Text = Properties.Settings.Default.MessageTemplate;
			cfgAPIKey.Text = Properties.Settings.Default.AccountSID;
			cfgAuthToken.Text = Properties.Settings.Default.AuthToken;
			cfgPhoneInput.Text = Properties.Settings.Default.PhoneNumber;

			statusLabel.Text = "Waiting for username";
		}

		// http://www.herlitz.nu/2012/04/24/creating-a-very-random-password-generator-in-c/
		private static readonly Random Random = new Random();
		private static string PasswordGenerator(int passwordLength) {
			
			int seed = Random.Next(1, int.MaxValue);
			const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

			var chars = new char[passwordLength];
			var rd = new Random(seed);

			for (var i = 0; i < passwordLength; i++) {
				chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
			}

			return new string(chars);
		}

		public void updateMessage() {
			string txt = inputMessageTemplate.Text;

			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("u", inputUsername.Text);
			parameters.Add("p", inputPassword.Text);
			txt = Regex.Replace(txt, @"%([a-z])", m => parameters[m.Groups[1].Value]);

			sMessage = txt;
			lblMessagePreview.Text = txt;

			if(	string.IsNullOrEmpty(Properties.Settings.Default.AccountSID) ||
				string.IsNullOrEmpty(Properties.Settings.Default.AuthToken) ||
				string.IsNullOrEmpty(Properties.Settings.Default.PhoneNumber) ) {
				btnSendMessage.Enabled = false;
				btnSendMessage.Text = "Twilio not configured";
			} else if ( string.IsNullOrEmpty(sUsername) || string.IsNullOrEmpty(sPassword) || string.IsNullOrEmpty(sPhone) || !ValidUser ) {
				btnSendMessage.Enabled = false;
			} else {
				btnSendMessage.Enabled = true;
				btnSendMessage.Text = "Send SMS to " + sPhone;
			}

			if (!ValidUser) {
				txtUserInfo.Text = "User info will appear here";
				btnUpdateUser.Enabled = false;
			}else {
				btnUpdateUser.Enabled = true;
			}

		}

		private void refreshUser(UserPrincipal user = null) {
			
			if(user == null) {

				if (adContext == null) {
					try {
						adContext = new PrincipalContext(ContextType.Domain);
					} catch (Exception ex) {
						statusLabel.Text = ex.Message;
						return;
					}
				}

				using (user = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername)) {
					if (user == null) return;
				}

			}

			txtUserInfo.Text = "Username: " + user.Name.ToString();
			txtUserInfo.Text += Environment.NewLine + "Name: " + (user.Name ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "Display name: " + (user.DisplayName ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "Description: " + (user.Description ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "---";
			txtUserInfo.Text += Environment.NewLine + "E-Mail: " + (user.EmailAddress ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "Phone: " + (user.VoiceTelephoneNumber ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "---";
			txtUserInfo.Text += Environment.NewLine + "Expiration: " + user.AccountExpirationDate.GetValueOrDefault().ToUniversalTime();
			txtUserInfo.Text += Environment.NewLine + "Lockout time: " + user.AccountLockoutTime.GetValueOrDefault().ToUniversalTime();
			txtUserInfo.Text += Environment.NewLine + "Bad logons: " + user.BadLogonCount.ToString();
			txtUserInfo.Text += Environment.NewLine + "Last bad pwd: " + user.LastBadPasswordAttempt.GetValueOrDefault().ToUniversalTime();
			txtUserInfo.Text += Environment.NewLine + "Last pwd set: " + user.LastPasswordSet.GetValueOrDefault().ToUniversalTime();
			txtUserInfo.Text += Environment.NewLine + "---";
			txtUserInfo.Text += Environment.NewLine + "Home dir: " + (user.HomeDirectory ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "Home drive: " + (user.HomeDrive ?? String.Empty).ToString();
			txtUserInfo.Text += Environment.NewLine + "---";
			txtUserInfo.Text += Environment.NewLine + "Refreshed: " + DateTime.Now.ToUniversalTime();

		}

		private void inputUsername_TextChanged(object sender, EventArgs e) {

			sUsername = inputUsername.Text;

			updateMessage();

			if (sUsername == String.Empty || sUsername.Length <= 3) {
				statusLabel.Text = "No username, or it's too short.";
				return;
			}

			statusLabel.Text = "Querying...";

			if (adContext == null) {
				try {
					adContext = new PrincipalContext(ContextType.Domain);
				} catch (Exception ex) {
					SystemSounds.Asterisk.Play();
					MessageBox.Show("AD DS Error: " + ex.Message);
					statusLabel.Text = "AD DS Error: " + ex.Message;
					return;
				}
			}

			sPhone = string.Empty;

			using (adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername)) {
				if(adUser != null) {
					statusLabel.Text = "Found user: " + adUser.Name + ", " + ( !string.IsNullOrEmpty(adUser.EmailAddress) ? adUser.EmailAddress : "no email" );
					if ( !string.IsNullOrEmpty(adUser.VoiceTelephoneNumber) ) {

						sPhone = adUser.VoiceTelephoneNumber;

						if(sPhone.Substring(0,1) != "+") {
							sPhone = "+46" + sPhone.Substring(1, sPhone.Length - 2);
						}

						if (!string.IsNullOrEmpty(sPassword)) {
							btnSendMessage.Text = "Send SMS to " + sPhone;
						}else {
							btnSendMessage.Text = "No password provided";
						}

						inputPhone.Text = sPhone;

						ValidUser = true;

						inputPassword.ReadOnly = false;
						btnChangePassword.Enabled = true;
						btnGenerate.Enabled = true;

					} else {
						inputPhone.Text = string.Empty;
						inputPassword.ReadOnly = true;
						btnChangePassword.Enabled = false;
						btnGenerate.Enabled = false;
						ValidUser = false;
						btnSendMessage.Text = "User has no phone number";
						updateMessage();
					}

					refreshUser(adUser);

					inputPhone.ReadOnly = false;
					btnSetPhone.Enabled = true;

				}else {
					inputPhone.Text = string.Empty;
					inputPhone.ReadOnly = true;
					btnSetPhone.Enabled = false;
					btnChangePassword.Enabled = false;
					btnGenerate.Enabled = false;
					inputPassword.ReadOnly = true;
					statusLabel.Text = "User not found";
					btnSendMessage.Text = "User not found";
					ValidUser = false;
					updateMessage();
				}
			}

		}

		private void inputMessageTemplate_TextChanged(object sender, EventArgs e) {
			updateMessage();
			lblMessageLength.Text = sMessage.Length + "/160";
		}

		private void inputPassword_TextChanged(object sender, EventArgs e) {
			sPassword = inputPassword.Text;
			
			if( string.IsNullOrEmpty(sPassword) || sPassword.Length < 7 ) {
				btnChangePassword.Enabled = false;
			}else {
				btnChangePassword.Enabled = true;
			}

			updateMessage();
		}

		private void btnGenerate_Click(object sender, EventArgs e) {
			sPassword = PasswordGenerator(8);
			inputPassword.Text = sPassword;
			updateMessage();
		}

		private void btnChangePassword_Click(object sender, EventArgs e) {

			if (adContext == null) {
				try {
					adContext = new PrincipalContext(ContextType.Domain);
				} catch (Exception ex) {
					SystemSounds.Asterisk.Play();
					MessageBox.Show("AD DS Error: " + ex.Message);
					statusLabel.Text = "AD DS Error: " + ex.Message;
					return;
				}
			}

			using (adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername)) {
				if (adUser != null) {

					try {
						adUser.SetPassword(sPassword);
					} catch (Exception ex) {
						statusLabel.Text = "Error: " + ex.Message;
						updateMessage();
						return;
					}

					adUser.Save();

					statusLabel.Text = "Changed password for " + sUsername;
					inputPassword.ReadOnly = true;
					inputUsername.ReadOnly = true;
					ValidUser = true;
					updateMessage();
					refreshUser();

				} else {
					statusLabel.Text = "User not found";
					btnSendMessage.Text = "User not found";
					ValidUser = false;
					updateMessage();
				}
			}
		}

		private void btnReset_Click(object sender, EventArgs e) {
			inputUsername.Text = String.Empty;
			inputPassword.Text = String.Empty;
			inputUsername.ReadOnly = false;
			inputPassword.ReadOnly = false;
			inputPhone.Text = String.Empty;
			inputPhone.ReadOnly = true;
			btnSetPhone.Enabled = false;
			btnChangePassword.Enabled = false;
			btnGenerate.Enabled = false;
			sUsername = String.Empty;
			sPassword = String.Empty;
			sPhone = String.Empty;
			ValidUser = false;
			inputMessageTemplate.Text = Properties.Settings.Default.MessageTemplate;
			statusLabel.Text = "Waiting for username";
			btnSendMessage.Enabled = false;
			btnSendMessage.Text = "Send SMS";
			txtUserInfo.Text = "User info will appear here";
			updateMessage();
		}

		private void btnSetPhone_Click(object sender, EventArgs e) {

			// if (string.IsNullOrEmpty(inputPhone.Text)) return;

			if (adContext == null) {
				try {
					adContext = new PrincipalContext(ContextType.Domain);
				} catch (Exception ex) {
					statusLabel.Text = ex.Message;
					return;
				}
			}

			using (adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername)) {
				if (adUser != null) {

					try {
						adUser.VoiceTelephoneNumber = inputPhone.Text;
					} catch (Exception ex) {
						statusLabel.Text = "Error: " + ex.Message;
						updateMessage();
						return;
					}

					adUser.Save();

					// adUser.Dispose();
					// context.Dispose();

					sPhone = inputPhone.Text;
					statusLabel.Text = "Changed phone for " + sUsername;
					MessageBox.Show("Changed phone for " + sUsername);
					updateMessage();

					refreshUser();

				} else {
					statusLabel.Text = "User not found";
					btnSendMessage.Text = "User not found";
					ValidUser = false;
					updateMessage();
				}
			}
		}

		private void cfgSave_Click(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(Properties.Settings.Default.AccountSID) ||
				string.IsNullOrEmpty(Properties.Settings.Default.AuthToken) ||
				string.IsNullOrEmpty(Properties.Settings.Default.PhoneNumber)) {
				MessageBox.Show("None of the fields can be left empty." + Environment.NewLine + "Check your Twilio console for your details.");
				return;
			}

			Properties.Settings.Default.AccountSID = cfgAPIKey.Text;
			Properties.Settings.Default.AuthToken = cfgAuthToken.Text;
			Properties.Settings.Default.PhoneNumber = cfgPhoneInput.Text;
			Properties.Settings.Default.Save();
			MessageBox.Show("Settings saved");
			updateMessage();

		}

		private void btnSaveTemplate_Click(object sender, EventArgs e) {
			Properties.Settings.Default.MessageTemplate = inputMessageTemplate.Text;
			Properties.Settings.Default.Save();
			MessageBox.Show("Template saved");
			updateMessage();
		}

		private void btnSendMessage_Click(object sender, EventArgs e) {

			string accountSid = Properties.Settings.Default.AccountSID;
			string authToken = Properties.Settings.Default.AuthToken;
			string phone = Properties.Settings.Default.PhoneNumber;

			if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(phone) ) {
				SystemSounds.Asterisk.Play();
				MessageBox.Show("Twilio not configured");
				return;
			}

			var twilio = new TwilioRestClient(accountSid, authToken);
			var message = twilio.SendMessage(phone,	sPhone,	sMessage);

			if(message.RestException != null) {
				SystemSounds.Asterisk.Play();
				MessageBox.Show("Message error: " + message.RestException.Message);
				statusLabel.Text = "Message error: " + message.RestException.Message;
				return;
			}

			MessageBox.Show("Message sent: " + message.Sid);
			statusLabel.Text = "Message sent: " + message.Sid;

		}

		private void btnUpdateUser_Click(object sender, EventArgs e) {
			refreshUser();
		}

		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) {

		}

		private void panelUser_Paint(object sender, PaintEventArgs e) {

		}
	}

}