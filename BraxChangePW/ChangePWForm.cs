using BraxChangePW.Properties;
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

		StringBuilder userInfo = new StringBuilder();

		PrincipalContext adContext;
		UserPrincipal adUser;

		public ChangePWForm() {
			InitializeComponent();

			// try connecting, show error if it doesn't work
			try {
				adContext = new PrincipalContext(ContextType.Domain);
			} catch (Exception ex) {
				MessageBox.Show("AD DS Error: " + ex.Message, "Could not connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
				statusLabel.Text = "AD DS Error: " + ex.Message;
				this.Text += " (error)";
			}

			// fill in default values
			inputMessageTemplate.Text = Properties.Settings.Default.MessageTemplate;
			inputCountryCode.Text = Properties.Settings.Default.CountryCode;
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
				btnChangeLogon.Enabled = false;
				btnUnlockAccount.Enabled = false;
			}else {
				btnUpdateUser.Enabled = true;
				btnChangeLogon.Enabled = true;
				btnUnlockAccount.Enabled = true;
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

			userInfo.Clear();
			userInfo.AppendLine("Username: " + user.SamAccountName );
			userInfo.AppendLine("Full name: " + user.Name );
			userInfo.AppendLine("Display name: " + user.DisplayName );
			userInfo.AppendLine("Description: " + user.Description );
			userInfo.AppendLine("Locked: " + ( user.IsAccountLockedOut() ? "Yes" : "No" ) );
			userInfo.AppendLine("---");
			userInfo.AppendLine("E-Mail: " + user.EmailAddress );
			userInfo.AppendLine("Phone: " + user.VoiceTelephoneNumber );
			userInfo.AppendLine("---");
			userInfo.AppendLine("Last logon: " + user.LastLogon.GetValueOrDefault().ToUniversalTime());
			userInfo.AppendLine("Expiration: " + user.AccountExpirationDate.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("Lockout time: " + user.AccountLockoutTime.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("Bad logons: " + user.BadLogonCount );
			userInfo.AppendLine("Last bad pwd: " + user.LastBadPasswordAttempt.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("Last pwd set: " + user.LastPasswordSet.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("---");
			userInfo.AppendLine("Home dir: " + user.HomeDirectory );
			userInfo.AppendLine("Home drive: " + user.HomeDrive );
			userInfo.AppendLine("---");
			userInfo.AppendLine("Refreshed: " + DateTime.UtcNow );

			txtUserInfo.Text = userInfo.ToString();

		}

		// main query + update
		private void inputUsername_TextChanged(object sender, EventArgs e) {

			sUsername = inputUsername.Text;

			updateMessage();

			// don't spam the db
			if (sUsername == String.Empty || sUsername.Length <= 3) {
				statusLabel.Text = "No username, or it's too short.";
				return;
			}

			statusLabel.Text = "Querying...";

			if (adContext == null) {
				try {
					adContext = new PrincipalContext(ContextType.Domain);
				} catch (Exception ex) {
					MessageBox.Show("AD DS Error: " + ex.Message, "Could not connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
					statusLabel.Text = "AD DS Error: " + ex.Message;
					return;
				}
			}

			sPhone = string.Empty; // reset phone

			// find user
			using (adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername)) {
				if(adUser != null) {
					statusLabel.Text = "Found user: " + adUser.Name + ", " + ( !string.IsNullOrEmpty(adUser.EmailAddress) ? adUser.EmailAddress : "no email" );
					if ( !string.IsNullOrEmpty(adUser.VoiceTelephoneNumber) ) {

						sPhone = adUser.VoiceTelephoneNumber;

						// replace first 0 with country code, if wrong format
						string cc = Properties.Settings.Default.CountryCode;
						if ( cc != string.Empty && sPhone.Substring(0,1) != "+") {
							sPhone = cc + sPhone.Substring(1, sPhone.Length - 2);
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
						
						// chkChangeLogon.Checked = adUser.unl

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

					btnChangeLogon.Enabled = true;

					if(!adUser.IsAccountLockedOut()) {
						btnUnlockAccount.Text = "Account unlocked";
						btnUnlockAccount.Enabled = false;
					}else {
						btnUnlockAccount.Text = "Unlock account";
						btnUnlockAccount.Enabled = true;
					}

				}else {
					inputPhone.Text = string.Empty;
					inputPhone.ReadOnly = true;
					btnSetPhone.Enabled = false;
					btnChangePassword.Enabled = false;
					btnGenerate.Enabled = false;
					inputPassword.ReadOnly = true;
					btnChangeLogon.Enabled = false;
					btnUnlockAccount.Enabled = false;
					btnUnlockAccount.Text = "Unlock account";
					statusLabel.Text = "User not found";
					btnSendMessage.Text = "User not found";
					ValidUser = false;
					updateMessage();
				}
			}

		}

		private bool tooLong = false;
		private void inputMessageTemplate_TextChanged(object sender, EventArgs e) {
			updateMessage();
			if (sMessage.Length > 160) {
				if (!tooLong) lblMessageLength.Font = new Font(lblMessageLength.Font, FontStyle.Bold);
				SystemSounds.Exclamation.Play();
				tooLong = true;
			} else {
				if (tooLong) {
					lblMessageLength.Font = new Font(lblMessageLength.Font, FontStyle.Regular);
					tooLong = false;
				}
			}
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

		// generate password, if you find a better way, please tell me :)
		private void btnGenerate_Click(object sender, EventArgs e) {
			sPassword = PasswordGenerator(8);
			inputPassword.Text = sPassword;
			updateMessage();
		}

		// change password in ad
		private void btnChangePassword_Click(object sender, EventArgs e) {

			if (adContext == null) {
				try {
					adContext = new PrincipalContext(ContextType.Domain);
				} catch (Exception ex) {
					MessageBox.Show("AD DS Error: " + ex.Message, "Password change failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				MessageBox.Show("None of the fields can be left empty." + Environment.NewLine + "Check your Twilio console for your details.", "Config missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			DialogResult q = MessageBox.Show("Are you sure you want to send?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (q == DialogResult.Cancel) return;

			string accountSid = Properties.Settings.Default.AccountSID;
			string authToken = Properties.Settings.Default.AuthToken;
			string phone = Properties.Settings.Default.PhoneNumber;

			// error if not configured
			if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(phone) ) {
				SystemSounds.Asterisk.Play();
				MessageBox.Show("Twilio not configured", "Could not send", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// twilio api
			var twilio = new TwilioRestClient(accountSid, authToken);
			var message = twilio.SendMessage(phone,	sPhone,	sMessage);

			// error return from request
			if(message.RestException != null) {
				MessageBox.Show("Twilio error: " + message.RestException.Message, "Message not sent", MessageBoxButtons.OK, MessageBoxIcon.Error);
				statusLabel.Text = "Twilio error: " + message.RestException.Message;
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

		private void inputCountryCode_TextChanged(object sender, EventArgs e) {
			Properties.Settings.Default.CountryCode = inputCountryCode.Text;
			Properties.Settings.Default.Save();
			statusLabel.Text = inputCountryCode.Text == string.Empty ? "Country code removed, will not modify phone now." : "Country code saved: " + inputCountryCode.Text;
		}

		private void btnChangeLogon_Click(object sender, EventArgs e) {
			if(adUser == null) {
				MessageBox.Show("No user object, please reload.", "Invalid request", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			adUser.ExpirePasswordNow();
			adUser.Save();
			statusLabel.Text = "Password expires, user now has to change on next logon.";
			MessageBox.Show("Password expires, user now has to change on next logon.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			refreshUser();
		}

		private void btnUnlockAccount_Click(object sender, EventArgs e) {
			if (adUser == null) {
				MessageBox.Show("No user object, please reload.", "Invalid request", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			adUser.UnlockAccount();
			adUser.Save();
			statusLabel.Text = "Account unlocked.";
			MessageBox.Show("Account unlocked.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			refreshUser();
		}

		private void ChangePWForm_Load(object sender, EventArgs e) {
			if (Settings.Default.WindowLocation != null) this.Location = Settings.Default.WindowLocation;
			if (Settings.Default.WindowSize != null) this.Size = Settings.Default.WindowSize;
		}

		private void ChangePWForm_FormClosing(object sender, FormClosingEventArgs e) {
			Settings.Default.WindowLocation = this.Location;
			if (this.WindowState == FormWindowState.Normal) {
				Settings.Default.WindowSize = this.Size;
			} else {
				Settings.Default.WindowSize = this.RestoreBounds.Size;
			}
			Settings.Default.Save();
		}
		
	}

}