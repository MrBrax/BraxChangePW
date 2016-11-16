namespace BraxChangePW {
	partial class ChangePWForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePWForm));
			this.cfgPhoneInput = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cfgSave = new System.Windows.Forms.Button();
			this.inputUsername = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.inputMessageTemplate = new System.Windows.Forms.TextBox();
			this.btnChangePassword = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.inputPassword = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.btnSendMessage = new System.Windows.Forms.Button();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.lblMessagePreview = new System.Windows.Forms.Label();
			this.cfgAPIKey = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.inputPhone = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnSetPhone = new System.Windows.Forms.Button();
			this.cfgAuthToken = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.lblMessageLength = new System.Windows.Forms.Label();
			this.txtUserInfo = new System.Windows.Forms.TextBox();
			this.btnUpdateUser = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panelUser = new System.Windows.Forms.Panel();
			this.panelMessage = new System.Windows.Forms.Panel();
			this.panelConfig = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panelUser.SuspendLayout();
			this.panelMessage.SuspendLayout();
			this.panelConfig.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// cfgPhoneInput
			// 
			this.cfgPhoneInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cfgPhoneInput.Location = new System.Drawing.Point(86, 0);
			this.cfgPhoneInput.Name = "cfgPhoneInput";
			this.cfgPhoneInput.Size = new System.Drawing.Size(266, 20);
			this.cfgPhoneInput.TabIndex = 22;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Phone number";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cfgSave
			// 
			this.cfgSave.Dock = System.Windows.Forms.DockStyle.Right;
			this.cfgSave.Location = new System.Drawing.Point(357, 27);
			this.cfgSave.Name = "cfgSave";
			this.cfgSave.Size = new System.Drawing.Size(52, 68);
			this.cfgSave.TabIndex = 23;
			this.cfgSave.Text = "Save";
			this.cfgSave.UseVisualStyleBackColor = true;
			this.cfgSave.Click += new System.EventHandler(this.cfgSave_Click);
			// 
			// inputUsername
			// 
			this.inputUsername.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputUsername.Location = new System.Drawing.Point(68, 3);
			this.inputUsername.Name = "inputUsername";
			this.inputUsername.Size = new System.Drawing.Size(333, 20);
			this.inputUsername.TabIndex = 0;
			this.inputUsername.TextChanged += new System.EventHandler(this.inputUsername_TextChanged);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Location = new System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Username";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 419);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(609, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(50, 17);
			this.statusLabel.Text = "Loading";
			// 
			// inputMessageTemplate
			// 
			this.inputMessageTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputMessageTemplate.Location = new System.Drawing.Point(3, 19);
			this.inputMessageTemplate.Multiline = true;
			this.inputMessageTemplate.Name = "inputMessageTemplate";
			this.inputMessageTemplate.Size = new System.Drawing.Size(190, 62);
			this.inputMessageTemplate.TabIndex = 5;
			this.inputMessageTemplate.TextChanged += new System.EventHandler(this.inputMessageTemplate_TextChanged);
			// 
			// btnChangePassword
			// 
			this.btnChangePassword.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnChangePassword.Enabled = false;
			this.btnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnChangePassword.Location = new System.Drawing.Point(326, 3);
			this.btnChangePassword.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.btnChangePassword.Name = "btnChangePassword";
			this.btnChangePassword.Size = new System.Drawing.Size(75, 20);
			this.btnChangePassword.TabIndex = 3;
			this.btnChangePassword.Text = "Change PW";
			this.btnChangePassword.UseVisualStyleBackColor = true;
			this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Location = new System.Drawing.Point(3, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// inputPassword
			// 
			this.inputPassword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputPassword.Location = new System.Drawing.Point(68, 3);
			this.inputPassword.Name = "inputPassword";
			this.inputPassword.ReadOnly = true;
			this.inputPassword.Size = new System.Drawing.Size(173, 20);
			this.inputPassword.TabIndex = 1;
			this.inputPassword.TextChanged += new System.EventHandler(this.inputPassword_TextChanged);
			// 
			// btnGenerate
			// 
			this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnGenerate.Enabled = false;
			this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerate.Location = new System.Drawing.Point(241, 3);
			this.btnGenerate.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(85, 20);
			this.btnGenerate.TabIndex = 2;
			this.btnGenerate.Text = "Generate PW";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// btnSendMessage
			// 
			this.btnSendMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnSendMessage.Enabled = false;
			this.btnSendMessage.Location = new System.Drawing.Point(5, 174);
			this.btnSendMessage.Name = "btnSendMessage";
			this.btnSendMessage.Size = new System.Drawing.Size(404, 36);
			this.btnSendMessage.TabIndex = 4;
			this.btnSendMessage.Text = "Send SMS";
			this.btnSendMessage.UseVisualStyleBackColor = true;
			this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnSaveTemplate.Location = new System.Drawing.Point(3, 81);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(190, 23);
			this.btnSaveTemplate.TabIndex = 11;
			this.btnSaveTemplate.Text = "Save template";
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(190, 16);
			this.label4.TabIndex = 12;
			this.label4.Text = "Message template";
			// 
			// lblMessagePreview
			// 
			this.lblMessagePreview.BackColor = System.Drawing.SystemColors.HighlightText;
			this.lblMessagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblMessagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMessagePreview.Location = new System.Drawing.Point(3, 19);
			this.lblMessagePreview.Name = "lblMessagePreview";
			this.lblMessagePreview.Size = new System.Drawing.Size(190, 62);
			this.lblMessagePreview.TabIndex = 13;
			this.lblMessagePreview.Text = "Loading";
			// 
			// cfgAPIKey
			// 
			this.cfgAPIKey.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cfgAPIKey.Location = new System.Drawing.Point(86, 0);
			this.cfgAPIKey.Name = "cfgAPIKey";
			this.cfgAPIKey.Size = new System.Drawing.Size(266, 20);
			this.cfgAPIKey.TabIndex = 20;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Left;
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "Account SID";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnReset
			// 
			this.btnReset.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnReset.Location = new System.Drawing.Point(5, 138);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(404, 36);
			this.btnReset.TabIndex = 14;
			this.btnReset.Text = "Reset form";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// inputPhone
			// 
			this.inputPhone.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputPhone.Location = new System.Drawing.Point(68, 3);
			this.inputPhone.Name = "inputPhone";
			this.inputPhone.ReadOnly = true;
			this.inputPhone.Size = new System.Drawing.Size(288, 20);
			this.inputPhone.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Left;
			this.label6.Location = new System.Drawing.Point(3, 3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 20);
			this.label6.TabIndex = 16;
			this.label6.Text = "Phone";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSetPhone
			// 
			this.btnSetPhone.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnSetPhone.Enabled = false;
			this.btnSetPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSetPhone.Location = new System.Drawing.Point(356, 3);
			this.btnSetPhone.Name = "btnSetPhone";
			this.btnSetPhone.Size = new System.Drawing.Size(45, 20);
			this.btnSetPhone.TabIndex = 17;
			this.btnSetPhone.Text = "Set";
			this.btnSetPhone.UseVisualStyleBackColor = true;
			this.btnSetPhone.Click += new System.EventHandler(this.btnSetPhone_Click);
			// 
			// cfgAuthToken
			// 
			this.cfgAuthToken.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cfgAuthToken.Location = new System.Drawing.Point(86, 0);
			this.cfgAuthToken.Name = "cfgAuthToken";
			this.cfgAuthToken.PasswordChar = '*';
			this.cfgAuthToken.Size = new System.Drawing.Size(266, 20);
			this.cfgAuthToken.TabIndex = 21;
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Left;
			this.label7.Location = new System.Drawing.Point(0, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(86, 23);
			this.label7.TabIndex = 6;
			this.label7.Text = "Auth Token";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMessageLength
			// 
			this.lblMessageLength.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblMessageLength.Location = new System.Drawing.Point(3, 81);
			this.lblMessageLength.Name = "lblMessageLength";
			this.lblMessageLength.Size = new System.Drawing.Size(190, 23);
			this.lblMessageLength.TabIndex = 18;
			this.lblMessageLength.Text = "0/160";
			this.lblMessageLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtUserInfo
			// 
			this.txtUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtUserInfo.Location = new System.Drawing.Point(0, 0);
			this.txtUserInfo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
			this.txtUserInfo.Multiline = true;
			this.txtUserInfo.Name = "txtUserInfo";
			this.txtUserInfo.ReadOnly = true;
			this.txtUserInfo.Size = new System.Drawing.Size(183, 386);
			this.txtUserInfo.TabIndex = 19;
			this.txtUserInfo.Text = "User info will appear here";
			// 
			// btnUpdateUser
			// 
			this.btnUpdateUser.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnUpdateUser.Enabled = false;
			this.btnUpdateUser.Location = new System.Drawing.Point(0, 386);
			this.btnUpdateUser.Name = "btnUpdateUser";
			this.btnUpdateUser.Size = new System.Drawing.Size(183, 29);
			this.btnUpdateUser.TabIndex = 20;
			this.btnUpdateUser.Text = "Update";
			this.btnUpdateUser.UseVisualStyleBackColor = true;
			this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Top;
			this.label8.Location = new System.Drawing.Point(3, 3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(190, 16);
			this.label8.TabIndex = 21;
			this.label8.Text = "Sent message";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panelMessage);
			this.splitContainer1.Panel1.Controls.Add(this.panelConfig);
			this.splitContainer1.Panel1.Controls.Add(this.panelUser);
			this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtUserInfo);
			this.splitContainer1.Panel2.Controls.Add(this.btnUpdateUser);
			this.splitContainer1.Size = new System.Drawing.Size(609, 419);
			this.splitContainer1.SplitterDistance = 418;
			this.splitContainer1.TabIndex = 22;
			// 
			// panelUser
			// 
			this.panelUser.Controls.Add(this.panel3);
			this.panelUser.Controls.Add(this.panel2);
			this.panelUser.Controls.Add(this.panel1);
			this.panelUser.Controls.Add(this.label11);
			this.panelUser.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelUser.Location = new System.Drawing.Point(0, 0);
			this.panelUser.Name = "panelUser";
			this.panelUser.Padding = new System.Windows.Forms.Padding(5);
			this.panelUser.Size = new System.Drawing.Size(414, 100);
			this.panelUser.TabIndex = 22;
			this.panelUser.Paint += new System.Windows.Forms.PaintEventHandler(this.panelUser_Paint);
			// 
			// panelMessage
			// 
			this.panelMessage.Controls.Add(this.splitContainer2);
			this.panelMessage.Controls.Add(this.label12);
			this.panelMessage.Controls.Add(this.label13);
			this.panelMessage.Controls.Add(this.btnReset);
			this.panelMessage.Controls.Add(this.btnSendMessage);
			this.panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMessage.Location = new System.Drawing.Point(0, 100);
			this.panelMessage.Name = "panelMessage";
			this.panelMessage.Padding = new System.Windows.Forms.Padding(5);
			this.panelMessage.Size = new System.Drawing.Size(414, 215);
			this.panelMessage.TabIndex = 23;
			// 
			// panelConfig
			// 
			this.panelConfig.Controls.Add(this.panel6);
			this.panelConfig.Controls.Add(this.panel5);
			this.panelConfig.Controls.Add(this.panel4);
			this.panelConfig.Controls.Add(this.cfgSave);
			this.panelConfig.Controls.Add(this.label9);
			this.panelConfig.Controls.Add(this.label10);
			this.panelConfig.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelConfig.Location = new System.Drawing.Point(0, 315);
			this.panelConfig.Name = "panelConfig";
			this.panelConfig.Padding = new System.Windows.Forms.Padding(5);
			this.panelConfig.Size = new System.Drawing.Size(414, 100);
			this.panelConfig.TabIndex = 24;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.inputUsername);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(5, 25);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(3);
			this.panel1.Size = new System.Drawing.Size(404, 26);
			this.panel1.TabIndex = 22;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.inputPhone);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.btnSetPhone);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(5, 51);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(3);
			this.panel2.Size = new System.Drawing.Size(404, 26);
			this.panel2.TabIndex = 23;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.inputPassword);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.btnGenerate);
			this.panel3.Controls.Add(this.btnChangePassword);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(5, 77);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new System.Windows.Forms.Padding(3);
			this.panel3.Size = new System.Drawing.Size(404, 26);
			this.panel3.TabIndex = 24;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(5, 27);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.inputMessageTemplate);
			this.splitContainer2.Panel1.Controls.Add(this.label4);
			this.splitContainer2.Panel1.Controls.Add(this.btnSaveTemplate);
			this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(3);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.lblMessagePreview);
			this.splitContainer2.Panel2.Controls.Add(this.label8);
			this.splitContainer2.Panel2.Controls.Add(this.lblMessageLength);
			this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(3);
			this.splitContainer2.Size = new System.Drawing.Size(404, 111);
			this.splitContainer2.SplitterDistance = 200;
			this.splitContainer2.TabIndex = 22;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.cfgAPIKey);
			this.panel4.Controls.Add(this.label5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(5, 27);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(352, 23);
			this.panel4.TabIndex = 24;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.cfgAuthToken);
			this.panel5.Controls.Add(this.label7);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(5, 50);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(352, 23);
			this.panel5.TabIndex = 25;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.cfgPhoneInput);
			this.panel6.Controls.Add(this.label1);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(5, 73);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(352, 23);
			this.panel6.TabIndex = 26;
			// 
			// label9
			// 
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(5, 7);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(404, 20);
			this.label9.TabIndex = 27;
			this.label9.Text = "Twilio settings";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label10.Dock = System.Windows.Forms.DockStyle.Top;
			this.label10.Location = new System.Drawing.Point(5, 5);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(404, 2);
			this.label10.TabIndex = 28;
			// 
			// label11
			// 
			this.label11.Dock = System.Windows.Forms.DockStyle.Top;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(5, 5);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(404, 20);
			this.label11.TabIndex = 28;
			this.label11.Text = "Find user && set password";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Dock = System.Windows.Forms.DockStyle.Top;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(5, 7);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(404, 20);
			this.label12.TabIndex = 28;
			this.label12.Text = "Message format";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label13.Dock = System.Windows.Forms.DockStyle.Top;
			this.label13.Location = new System.Drawing.Point(5, 5);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(404, 2);
			this.label13.TabIndex = 29;
			// 
			// ChangePWForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(609, 441);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ChangePWForm";
			this.Text = "BraxChangePW";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panelUser.ResumeLayout(false);
			this.panelMessage.ResumeLayout(false);
			this.panelConfig.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox cfgPhoneInput;
		private System.Windows.Forms.Button cfgSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox inputUsername;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.TextBox inputMessageTemplate;
		private System.Windows.Forms.Button btnChangePassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox inputPassword;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.Button btnSendMessage;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblMessagePreview;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox cfgAPIKey;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.TextBox inputPhone;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSetPhone;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox cfgAuthToken;
		private System.Windows.Forms.Label lblMessageLength;
		private System.Windows.Forms.TextBox txtUserInfo;
		private System.Windows.Forms.Button btnUpdateUser;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panelUser;
		private System.Windows.Forms.Panel panelConfig;
		private System.Windows.Forms.Panel panelMessage;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
	}
}

