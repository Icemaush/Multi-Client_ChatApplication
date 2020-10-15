using System;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginForm : Form
    {
        public LoginForm(Form form)
        {
            InitializeComponent();
        }

        // Connect to server
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ValidateInfo();
        }

        // Validate user input
        private void ValidateInfo ()
        {
            if (string.IsNullOrWhiteSpace(textUsername.Text) || string.IsNullOrWhiteSpace(textPassword.Text) || string.IsNullOrWhiteSpace(textServer.Text))
            {
                lblError.Text = "All fields required.";
            }
        }
    }
}
