namespace Server
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupCreateUser = new System.Windows.Forms.GroupBox();
            this.btnDisplayUsers = new System.Windows.Forms.Button();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textMessages = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupServer = new System.Windows.Forms.GroupBox();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupSendMessage = new System.Windows.Forms.GroupBox();
            this.textSendMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.groupCreateUser.SuspendLayout();
            this.groupServer.SuspendLayout();
            this.groupSendMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupCreateUser
            // 
            this.groupCreateUser.Controls.Add(this.btnDisplayUsers);
            this.groupCreateUser.Controls.Add(this.btnRemoveUser);
            this.groupCreateUser.Controls.Add(this.btnResetPassword);
            this.groupCreateUser.Controls.Add(this.textPassword);
            this.groupCreateUser.Controls.Add(this.textUsername);
            this.groupCreateUser.Controls.Add(this.btnAddUser);
            this.groupCreateUser.Controls.Add(this.label2);
            this.groupCreateUser.Controls.Add(this.label1);
            this.groupCreateUser.Location = new System.Drawing.Point(12, 116);
            this.groupCreateUser.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.groupCreateUser.Name = "groupCreateUser";
            this.groupCreateUser.Size = new System.Drawing.Size(227, 153);
            this.groupCreateUser.TabIndex = 0;
            this.groupCreateUser.TabStop = false;
            this.groupCreateUser.Text = "Create User";
            // 
            // btnDisplayUsers
            // 
            this.btnDisplayUsers.Enabled = false;
            this.btnDisplayUsers.Location = new System.Drawing.Point(113, 116);
            this.btnDisplayUsers.Name = "btnDisplayUsers";
            this.btnDisplayUsers.Size = new System.Drawing.Size(95, 23);
            this.btnDisplayUsers.TabIndex = 7;
            this.btnDisplayUsers.Text = "Display Users";
            this.btnDisplayUsers.UseVisualStyleBackColor = true;
            this.btnDisplayUsers.Click += new System.EventHandler(this.btnDisplayUsers_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Enabled = false;
            this.btnRemoveUser.Location = new System.Drawing.Point(113, 87);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(95, 23);
            this.btnRemoveUser.TabIndex = 6;
            this.btnRemoveUser.Text = "Remove User";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Enabled = false;
            this.btnResetPassword.Location = new System.Drawing.Point(9, 116);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(95, 23);
            this.btnResetPassword.TabIndex = 5;
            this.btnResetPassword.Text = "ResetPassword";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // textPassword
            // 
            this.textPassword.Enabled = false;
            this.textPassword.Location = new System.Drawing.Point(70, 51);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(138, 20);
            this.textPassword.TabIndex = 4;
            // 
            // textUsername
            // 
            this.textUsername.Enabled = false;
            this.textUsername.Location = new System.Drawing.Point(70, 23);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(138, 20);
            this.textUsername.TabIndex = 3;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Enabled = false;
            this.btnAddUser.Location = new System.Drawing.Point(9, 87);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(95, 23);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // textMessages
            // 
            this.textMessages.Location = new System.Drawing.Point(260, 12);
            this.textMessages.Multiline = true;
            this.textMessages.Name = "textMessages";
            this.textMessages.ReadOnly = true;
            this.textMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textMessages.Size = new System.Drawing.Size(362, 403);
            this.textMessages.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(6, 49);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupServer
            // 
            this.groupServer.Controls.Add(this.lblServerStatus);
            this.groupServer.Controls.Add(this.label3);
            this.groupServer.Controls.Add(this.btnStart);
            this.groupServer.Controls.Add(this.btnStop);
            this.groupServer.Location = new System.Drawing.Point(12, 12);
            this.groupServer.Name = "groupServer";
            this.groupServer.Size = new System.Drawing.Size(227, 86);
            this.groupServer.TabIndex = 4;
            this.groupServer.TabStop = false;
            this.groupServer.Text = "Server";
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerStatus.ForeColor = System.Drawing.Color.Red;
            this.lblServerStatus.Location = new System.Drawing.Point(144, 49);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(56, 17);
            this.lblServerStatus.TabIndex = 5;
            this.lblServerStatus.Text = "Offline";
            this.lblServerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(127, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server Status";
            // 
            // groupSendMessage
            // 
            this.groupSendMessage.Controls.Add(this.textSendMessage);
            this.groupSendMessage.Controls.Add(this.btnSendMessage);
            this.groupSendMessage.Location = new System.Drawing.Point(12, 287);
            this.groupSendMessage.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.groupSendMessage.Name = "groupSendMessage";
            this.groupSendMessage.Size = new System.Drawing.Size(227, 128);
            this.groupSendMessage.TabIndex = 5;
            this.groupSendMessage.TabStop = false;
            this.groupSendMessage.Text = "Send Message";
            // 
            // textSendMessage
            // 
            this.textSendMessage.Enabled = false;
            this.textSendMessage.Location = new System.Drawing.Point(11, 23);
            this.textSendMessage.Multiline = true;
            this.textSendMessage.Name = "textSendMessage";
            this.textSendMessage.Size = new System.Drawing.Size(197, 60);
            this.textSendMessage.TabIndex = 3;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Enabled = false;
            this.btnSendMessage.Location = new System.Drawing.Point(11, 89);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // Form
            // 
            this.AcceptButton = this.btnSendMessage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 429);
            this.Controls.Add(this.groupSendMessage);
            this.Controls.Add(this.groupServer);
            this.Controls.Add(this.textMessages);
            this.Controls.Add(this.groupCreateUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.groupCreateUser.ResumeLayout(false);
            this.groupCreateUser.PerformLayout();
            this.groupServer.ResumeLayout(false);
            this.groupServer.PerformLayout();
            this.groupSendMessage.ResumeLayout(false);
            this.groupSendMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupCreateUser;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupSendMessage;
        private System.Windows.Forms.TextBox textSendMessage;
        private System.Windows.Forms.Button btnSendMessage;
        public System.Windows.Forms.Label lblServerStatus;
        public System.Windows.Forms.TextBox textMessages;
        private System.Windows.Forms.Button btnDisplayUsers;
        private System.Windows.Forms.Button btnRemoveUser;
    }
}

