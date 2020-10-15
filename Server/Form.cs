using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Form : System.Windows.Forms.Form
    {
        private delegate void SafeCallDelegate(string text);
        private Thread thread;
        private Server server;

        public Form()
        {
            InitializeComponent();
            server = new Server(this);
        }

        // Start the server
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (server.Status.Equals("OFFLINE")) {
                thread = new Thread(server.StartServer);
                thread.Start();
                EnableControls();
                UpdateStatus("ONLINE");
            }
        }

        // Stop the server
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.Status.Equals("ONLINE"))
            {
                server.StopServer();
                UpdateStatus("OFFLINE");
                DisableControlsSafe("");
                thread.Abort();
            }
        }

        // Set server status text
        private void UpdateStatus(string status)
        {
            if (status.Equals("ONLINE"))
            {
                lblServerStatus.ForeColor = Color.Green;
                lblServerStatus.Text = "ONLINE";
            } else
            {
                lblServerStatus.ForeColor = Color.Red;
                lblServerStatus.Text = "OFFLINE";
            }
        }

        // Add message to chat window
        public void AppendMessage(string text)
        {
            WriteTextSafe(text);
        }

        // Update chat window messages, thread safe
        private void WriteTextSafe(string text)
        {
            if (textMessages.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                textMessages.Invoke(d, new object[] { text });
            }
            else
            {
                textMessages.AppendText(text);
                textMessages.AppendText(Environment.NewLine);
            }
        }

        // Enables server controls
        private void EnableControls()
        {
            foreach (Control ctrl in groupCreateUser.Controls)
            {
                ctrl.Enabled = true;
            }

            foreach (Control ctrl in groupSendMessage.Controls)
            {
                ctrl.Enabled = true;
            }
        }

        // Disable form controls when server is offline, thread safe
        private void DisableControlsSafe(string text)
        {
            if (InvokeRequired)
            {
                var d = new SafeCallDelegate(DisableControlsSafe);
                Invoke(d, new object[] { text });

            }
            else
            {
                foreach (Control ctrl in groupCreateUser.Controls)
                {
                        ctrl.Enabled = false;
                }

                foreach (Control ctrl in groupSendMessage.Controls)
                {
                    ctrl.Enabled = false;
                }
            }
        }

        // Perform when form is closed
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            server.StopServer();
            thread.Abort();
        }

        // Send system (server) message to all clients
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textSendMessage.Text))
            {
                server.SendServerMessage(textSendMessage.Text);
                textSendMessage.Text = "";
            }
        }

        // Add user
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (ValidateUserInfo(textUsername.Text, textPassword.Text))
            {
                server.AddUser(textUsername.Text, textPassword.Text);
                ClearUserFields();
            }
        }

        // Validate user information
        private bool ValidateUserInfo(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                AppendMessage("Enter username AND password.");
                return false;
            } else if (username.Length < 3)
            {
                AppendMessage("Username must be at least 3 characters.");
                return false;
            } else if (password.Length < 4)
            {
                AppendMessage("Password must be at least 4 characters.");
                return false;
            } else
            {
                return true;
            }
        }

        // Remove a user
        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textUsername.Text)) {
                AppendMessage("Enter username.");
            } else
            {
                server.RemoveUser(textUsername.Text);
                ClearUserFields();
            }
        }

        // Display all users
        private void btnDisplayUsers_Click(object sender, EventArgs e)
        {
            server.DisplayUsers();
        }

        // Reset user password
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            server.ResetUserPassword(textUsername.Text, textPassword.Text);
            ClearUserFields();
        }

        // Clear user fields
        private void ClearUserFields()
        {
            textUsername.Text = "";
            textPassword.Text = "";
            textUsername.Focus();
        }
    }
}
