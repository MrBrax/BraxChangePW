using BraxChangePW.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;

namespace BraxChangePW {

	public partial class ChangePWForm : Form {

        public string textLog = "";

		public string sUsername;
		public string sPassword;
		public string sMessage;
		public string sPhone;

		public bool ValidUser = false;
		public bool FoundUser = false;
        public bool UserHasPhone = false;
        public bool PasswordChanged = false;

		StringBuilder userInfo = new StringBuilder();

		PrincipalContext adContext;
		UserPrincipal adUser;

		public ChangePWForm() {

			InitializeComponent();

            this.Text = "BraxChangePW " + Assembly.GetExecutingAssembly().GetName().Version;

            // try connecting, show error if it doesn't work
            /*
            try {
				adContext = new PrincipalContext(ContextType.Domain);
			} catch (Exception ex) {
				MessageBox.Show("AD DS Error: " + ex.Message, "Could not connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
				statusLabel.Text = "AD DS Error: " + ex.Message;
				this.Text += " (error)";
			}
            */
            adConnect();

			// fill in default values
			inputMessageTemplate.Text = Properties.Settings.Default.MessageTemplate;
			inputCountryCode.Text = Properties.Settings.Default.CountryCode;
			cfgAPIKey.Text = Properties.Settings.Default.AccountSID;
			cfgAuthToken.Text = Properties.Settings.Default.AuthToken;
			cfgPhoneInput.Text = Properties.Settings.Default.PhoneNumber;

            // statusLabel.Text = "Waiting for username";
            resetForm();

            doLog("Ready");

        }

        public void doLog( string text ) {
            string time = DateTime.Now.ToString("yyyyMMdd|HH:mm:ss");
            textLog += "[" + time + "] " + text + Environment.NewLine;
            logTextBox.Text = textLog;
            logTextBox.ScrollToCaret();
        }

        public void resetForm() {

            inputUsername.Text = String.Empty;
            inputUsername.ReadOnly = false;

            inputPassword.Text = String.Empty;
            inputPassword.ReadOnly = true;

            inputPhone.Text = String.Empty;
            inputPhone.ReadOnly = true;

            btnSetPhone.Enabled = false;
            btnChangePassword.Enabled = false;
            btnGenerate.Enabled = false;

            sUsername = String.Empty;
            sPassword = String.Empty;
            sPhone = String.Empty;

            ValidUser = false;
            FoundUser = false;
            UserHasPhone = false;
            PasswordChanged = false;

            inputMessageTemplate.Text = Properties.Settings.Default.MessageTemplate;

            setStatus("Waiting for username");

            btnSendMessage.Enabled = false;
            btnSendMessage.Text = "Send SMS";

            txtUserInfo.Text = "User info will appear here";

            updateMessage();

            doLog("Form reset");

        }

        private bool adConnect() {
			if (adContext == null) {
				try {
					adContext = new PrincipalContext(ContextType.Domain);
				} catch (Exception ex) {
					MessageBox.Show("AD DS Error: " + ex.Message + Environment.NewLine + Environment.NewLine + "Program will run regardless.", "Could not connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
					statusLabel.Text = "AD DS Error: " + ex.Message;
					statusContext.Text = "CONN ERR";
                    doLog("Failed connecting to AD");
                    return false;
				}
				statusContext.Text = "CONN OK";
                doLog("Connected to AD");
            } else {
                // doLog("Already connected to AD");
            }
			return true;
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
			parameters.Add("u", sUsername);
			parameters.Add("p", PasswordChanged ? "<NOCHANGE>" : sPassword);
			txt = Regex.Replace(txt, @"%([a-z])", m => parameters[m.Groups[1].Value]);

			sMessage = txt;
			lblMessagePreview.Text = txt;

            // send message button
			if(	string.IsNullOrEmpty(Properties.Settings.Default.AccountSID) ||
				
                // twilio not configured
                string.IsNullOrEmpty(Properties.Settings.Default.AuthToken) ||
				string.IsNullOrEmpty(Properties.Settings.Default.PhoneNumber) ) {
				btnSendMessage.Enabled = false;
				btnSendMessage.Text = "Twilio not configured";

			} else if ( string.IsNullOrEmpty(sUsername) || string.IsNullOrEmpty(sPassword) || string.IsNullOrEmpty(sPhone) || !ValidUser ) {

                btnSendMessage.Enabled = false;
				btnSendMessage.Text = "Can't send, something is missing.";

			} else {

				btnSendMessage.Enabled = true;
				btnSendMessage.Text = "Send SMS to " + sPhone;

			}


			if (!ValidUser) {
				txtUserInfo.Text = "Non-fitting user found";
				btnForceChangeLogon.Enabled = false;
				btnUnlockAccount.Enabled = false;
			}else {
				btnForceChangeLogon.Enabled = true;
				btnUnlockAccount.Enabled = true;
			}

            lblPhoneNumber.ForeColor = SystemColors.ControlText;

            if (!FoundUser) {
				btnUpdateUser.Enabled = false;
				txtUserInfo.Text = "No user found";
				lblUsername.ForeColor = System.Drawing.Color.Red;
			} else {
				btnUpdateUser.Enabled = true;
				lblUsername.ForeColor = SystemColors.ControlText;

                if (!UserHasPhone) {
                    lblPhoneNumber.ForeColor = System.Drawing.Color.Red;
                }

			}

			statusContext.Text = "CONN " + (adContext == null ? "ERR" : "OK");
			statusUser.Text = "USR " + (adUser == null ? "ERR" : "OK");

		}

		private void refreshUser() {
			
			if(adUser == null) {
				if (!adConnect()) return;
				adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername);
			}

			userInfo.Clear();
			userInfo.AppendLine("Username: " + adUser.SamAccountName );
			userInfo.AppendLine("Full name: " + adUser.Name );
			userInfo.AppendLine("Display name: " + adUser.DisplayName );
			userInfo.AppendLine("Description: " + adUser.Description );
			userInfo.AppendLine("Locked: " + ( adUser.IsAccountLockedOut() ? "Yes" : "No" ) );
			userInfo.AppendLine("---");
			userInfo.AppendLine("E-Mail: " + adUser.EmailAddress );
			userInfo.AppendLine("Phone: " + adUser.VoiceTelephoneNumber );
			userInfo.AppendLine("---");
			userInfo.AppendLine("Bad logons: " + adUser.BadLogonCount);
			userInfo.AppendLine("Last logon: " + adUser.LastLogon.GetValueOrDefault().ToUniversalTime());
			userInfo.AppendLine("Expiration: " + adUser.AccountExpirationDate.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("Lockout time: " + adUser.AccountLockoutTime.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("Last bad pwd: " + adUser.LastBadPasswordAttempt.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("Last pwd set: " + adUser.LastPasswordSet.GetValueOrDefault().ToUniversalTime() );
			userInfo.AppendLine("---");
			userInfo.AppendLine("Home dir: " + adUser.HomeDirectory );
			userInfo.AppendLine("Home drive: " + adUser.HomeDrive );
			userInfo.AppendLine("---");
			userInfo.AppendLine("Refreshed: " + DateTime.UtcNow );

			txtUserInfo.Text = userInfo.ToString();

		}

        private void setStatus( string sText) {
            statusLabel.Text = sText;
        }

        private bool checkUser( string chkUserName ) {

            updateMessage();

            // garbage
            if (adUser != null)
                adUser.Dispose();

            // don't spam the db
            if (chkUserName == String.Empty || chkUserName.Length <= 3) {
                setStatus("No username, or it's too short.");
                return false;
            }

            setStatus("Querying...");
                
            // try to connect
            if (!adConnect()) {
                setStatus("AD connection error");
                return false;
            }

            sPhone = string.Empty; // reset phone			

            // find user
            adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, chkUserName);

            FoundUser = adUser != null;

            if (FoundUser) {

                setStatus( "Found user: " + adUser.Name + ", " + (!string.IsNullOrEmpty(adUser.EmailAddress) ? adUser.EmailAddress : "no email") );

                if (!string.IsNullOrEmpty(adUser.VoiceTelephoneNumber)) {

                    sPhone = adUser.VoiceTelephoneNumber;

                    // replace first 0 with country code, if wrong format
                    string cc = Properties.Settings.Default.CountryCode;
                    if (cc != string.Empty && sPhone.Substring(0, 1) != "+") {
                        sPhone = cc + sPhone.Substring(1, sPhone.Length - 2);
                    }

                    if (!string.IsNullOrEmpty(sPassword)) {
                        btnSendMessage.Text = "Send SMS to " + sPhone;
                    } else {
                        btnSendMessage.Text = "No password provided";
                    }

                    inputPhone.Text = sPhone;
                    inputPassword.ReadOnly = false;

                    btnChangePassword.Enabled = true;
                    btnGenerate.Enabled = true;

                    ValidUser = true;

                    UserHasPhone = true;

                    doLog("User '" + chkUserName + "' OK!");

                } else {

                    inputPhone.Text = string.Empty;
                    inputPassword.ReadOnly = true;

                    btnChangePassword.Enabled = false;
                    btnGenerate.Enabled = false;

                    ValidUser = false;

                    UserHasPhone = false;

                    btnSendMessage.Text = "User has no phone number";

                    doLog("User '" + chkUserName + "' has no phone number");

                }

                sUsername = chkUserName;

                updateMessage();

                refreshUser();

                inputPhone.ReadOnly = false;
                btnSetPhone.Enabled = true;

                btnForceChangeLogon.Enabled = true;
                btnUnlockAccount.Enabled = true;

                if (!adUser.IsAccountLockedOut()) {
                    btnUnlockAccount.Text = "Account unlocked";
                } else {
                    btnUnlockAccount.Text = "Unlock account";
                }

                return ValidUser && UserHasPhone;

            } else {

                inputPhone.Text = string.Empty;
                inputPhone.ReadOnly = true;
                inputPassword.ReadOnly = true;

                btnSetPhone.Enabled = false;
                btnChangePassword.Enabled = false;
                btnGenerate.Enabled = false;
                btnForceChangeLogon.Enabled = false;
                btnUnlockAccount.Enabled = false;
                btnUnlockAccount.Text = "Unlock account";
                btnSendMessage.Text = "User not found";

                setStatus("User not found");
                
                ValidUser = false;

                updateMessage();

                return false;

            }

        }

		// main query + update
		private void inputUsername_TextChanged(object sender, EventArgs e) {
            bool okUser = checkUser(inputUsername.Text);
		}

        // check message template
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

            PasswordChanged = false;

            inputPassword.Text = sPassword;

            updateMessage();

            doLog("Password generated");

		}

		// change password in ad
		private void btnChangePassword_Click(object sender, EventArgs e) {

			if (!adConnect()) return;

            if (adUser == null) {
                adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername);
            }

            inputPassword.ReadOnly = true;
            btnChangePassword.Enabled = false;
            inputUsername.ReadOnly = true;

            try {
				adUser.SetPassword(sPassword);
			} catch (Exception ex) {

                inputPassword.ReadOnly = false;
                btnChangePassword.Enabled = true;

                statusLabel.Text = "Set password error: " + ex.Message;
                doLog("Set password error: " + ex.Message);
                MessageBox.Show(ex.Message, "Set password error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updateMessage();
				return;
			}

            doLog("Password changed for '" + sUsername + "'");

			adUser.Save();

			setStatus("Changed password for " + sUsername);

			updateMessage();

			refreshUser();

		}

		private void btnReset_Click(object sender, EventArgs e) {
            resetForm();
		}

		// changes db
		private void btnSetPhone_Click(object sender, EventArgs e) {

			// if (string.IsNullOrEmpty(inputPhone.Text)) return;

			if (!adConnect()) return;

			if(adUser == null) adUser = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, sUsername);

			try {
				adUser.VoiceTelephoneNumber = inputPhone.Text;
			} catch (Exception ex) {
				setStatus("Set phone error: " + ex.Message);
                doLog("Set phone error: " + ex.Message);
                MessageBox.Show(ex.Message, "Set phone error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updateMessage();
				return;
			}

			adUser.Save();

			sPhone = inputPhone.Text;

			setStatus("Changed phone for '" + sUsername + "'");

			MessageBox.Show("Changed phone for '" + sUsername + "'");

            doLog("Changed phone for '" + sUsername + "'");

            updateMessage();

			refreshUser();

		}

		private void cfgSave_Click(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(cfgAPIKey.Text) ||
				string.IsNullOrEmpty(cfgAuthToken.Text) ||
				string.IsNullOrEmpty(cfgPhoneInput.Text)) {
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
                setStatus("Twilio not configured");
                doLog("Twilio not configured");
				return;
			}

			// twilio api
			var twilio = new TwilioRestClient(accountSid, authToken);
			var message = twilio.SendMessage(phone,	sPhone,	sMessage);

			// error return from request
			if(message.RestException != null) {
				MessageBox.Show("Twilio error: " + message.RestException.Message, "Message not sent", MessageBoxButtons.OK, MessageBoxIcon.Error);
				setStatus("Twilio error: " + message.RestException.Message);
                doLog("Twilio error: " + message.RestException.Message);
				return;
			}

			MessageBox.Show("Message sent." + Environment.NewLine + "SID: " + message.Sid);
			setStatus("Message sent to " + sPhone + ", SID " + message.Sid);
            doLog("Message sent to " + sPhone + ", SID " + message.Sid);

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

		// changes db
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

		// changes db
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