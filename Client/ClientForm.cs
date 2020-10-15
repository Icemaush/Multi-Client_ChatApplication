using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        private delegate void SafeCallDelegate(string text);
        private TcpClient clientSocket = null;
        private NetworkStream networkStream = null;
        public string username = null;
        private bool listening = true;
        private BackgroundWorker backgroundWorker = null;
        private Form loginForm = null;

        public ClientForm()
        {
            InitializeComponent();
            DisableControlsSafe("");
        }

        // Connect to server
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSocket == null)
                {
                    clientSocket = new TcpClient();

                    // Create Modal dialog to prompt user for login information
                    if (loginForm == null)
                    {
                        loginForm = new LoginForm(this);
                        loginForm.Location = new Point(this.PointToScreen(Point.Empty).X + this.Width, this.PointToScreen(Point.Empty).Y);
                    }
                    loginForm.ShowDialog();

                    if (loginForm.DialogResult == DialogResult.OK)
                    {
                        // Retrieve information from login form
                        username = loginForm.Controls.Find("textUsername", true)[0].Text;
                        string password = loginForm.Controls.Find("textPassword", true)[0].Text;
                        string serverAddress = loginForm.Controls.Find("textServer", true)[0].Text;
                        char[] separator = { ':' };
                        string serverIP = serverAddress.Split(separator)[0];
                        int serverPort = Convert.ToInt32(serverAddress.Split(separator)[1]);

                        clientSocket.Connect(serverIP, serverPort);
                        networkStream = clientSocket.GetStream();
                        listening = true;
                        SetupBackgroundWorker();

                        SendToServer("/uname " + username + ":/password " + password);

                        EnableControls();
                        UpdateTitleSafe("Client - " + username);
                        AppendMessage("Connected to server.");
                        textMessage.Focus();
                    } else
                    {
                        loginForm = null;
                        clientSocket = null;
                    }
                }
            } catch (Exception ex)
            {
                clientSocket = null;
                AppendMessage("Unable to connect to server.");
                Console.WriteLine(ex.StackTrace);
            }
        }

        // Disconnect from server
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        // Send message to server
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textMessage.Text))
            {
                SendToServer(textMessage.Text);
                if (!textMessage.Text.StartsWith("/"))
                {
                    AppendMessage("[" + username + "]: " + textMessage.Text);
                }
                else if (textMessage.Text.StartsWith("/w "))
                {
                    char[] separator = { ' ' };
                    AppendMessage("To [" + textMessage.Text.Split(separator)[1] + "]: " + textMessage.Text.Substring(3 + textMessage.Text.Split(separator)[1].Length + 1));
                }
                textMessage.Text = "";
            }
        }

        // Read incoming data from server
        private string ReadFromServer()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inStream = new byte[8196];
                ms.Write(inStream, 0, networkStream.Read(inStream, 0, inStream.Length));
                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }

        // Write data to network stream 
        private void SendToServer(string message)
        {
            try
            {
                byte[] outStream = Encoding.ASCII.GetBytes(message + "\r");
                networkStream.Write(outStream, 0, outStream.Length);
            } catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Listen for incoming data from server
        private void Listen()
        {
            string line = null;

            while (listening == true)
            {
                try
                {
                    if (clientSocket.Available > 0)
                    {
                        // Read from server
                        line = ReadFromServer();
                        
                        // Check for username command
                        if (line.StartsWith("/uname"))
                        {
                            CheckUsernameStatus(line);
                            continue;
                        }

                        // Check for whisper command
                        if (line.StartsWith("/w"))
                        {
                            string sender = line.Substring(line.IndexOf('[') + 1, (line.IndexOf(']') - 1) - line.IndexOf('['));

                            line = line.Substring(3);
                            AppendMessage(line);
                            continue;
                        }

                        // Check for disconnect command
                        if (line.StartsWith("/disconnect"))
                        {
                            Disconnect();
                            break;
                        }

                        AppendMessage(line);
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        // Disconnect from server
        private void Disconnect()
        {
            try
            {
                if (clientSocket != null)
                {
                    // Send disconnect message to server
                    SendToServer("/quit");

                    listening = false;
                    clientSocket.Close();
                    
                    if (backgroundWorker != null)
                    {
                        backgroundWorker.Dispose();
                    }
                }

                clientSocket = null;
                networkStream = null;
                username = null;
                loginForm = null;
                UpdateTitleSafe("Client");
                AppendMessage("Disconnected from server.");
                DisableControlsSafe("");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
        }

        // Enables client controls
        private void EnableControls()
        {
            foreach (Control ctrl in Controls)
            {
                ctrl.Enabled = true;
            }
        }

        // Add message to chat window
        public void AppendMessage(string message)
        {
            WriteTextSafe(message);
        }

        // Update chat window messages, thread safe
        private void WriteTextSafe(string text)
        {
            if (textChat.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                textChat.Invoke(d, new object[] { text });
            }
            else
            {
                textChat.AppendText(text);
                textChat.AppendText(Environment.NewLine);
            }
        }

        // Disable form controls when not connected, thread safe
        private void DisableControlsSafe(string text)
        {
            if (InvokeRequired)
            {
                var d = new SafeCallDelegate(DisableControlsSafe);
                Invoke(d, new object[] { text });
            } else
            {
                foreach (Control ctrl in Controls)
                {
                    if (!ctrl.Name.Equals("btnConnect"))
                    {
                        ctrl.Enabled = false;
                    }
                }
            }
        }

        // Update form title
        private void UpdateTitleSafe (string text)
        {
            if (InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateTitleSafe);
                Invoke(d, new object[] { text });
            }
            else
            {
                Text = text;
            }
        }

        // Check username status response from server
        private void CheckUsernameStatus(string line)
        {
            char[] separator = { ' ' };
            string status = line.Split(separator)[1];

            if (status.Substring(0, 1).Equals("0")) 
            {
                Disconnect();
            }
        }

        // Setup background worker to perform tasks asynchronously in the background
        private void SetupBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerAsync();
        }

        // Background worker tasks to perform
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Listen();
        }

        // Perform when form is closed
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (clientSocket != null)
            {
                Disconnect();
            }
        }
    }
}
