using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ClientHandler
    {
        private Server server;
        private Form form;
        private TcpClient clientSocket;
        private Thread thread;
        private NetworkStream networkStream;
        public string username = "";
        private bool listening = true;

        public ClientHandler(Form form, Server server)
        {
            this.server = server;
            this.form = form;
        }

        // Connects a client to the client handler.
        public void Connect(TcpClient clientSocket)
        {
            this.clientSocket = clientSocket;
            networkStream = clientSocket.GetStream();
            thread = new Thread(Listen);
            thread.Start();

        }

        // Listen for incoming data from client
        private void Listen()
        {
            string line = null;
            
            while (listening == true)
            {
                try
                {
                    if (clientSocket.Available > 0)
                    {
                        // Read from client
                        line = ReadFromClient();

                        if (line.StartsWith("/uname"))
                        {
                            VerifyUserCredentials(line);
                            continue;
                        }

                        if (line.StartsWith("/w")) 
                        {
                            char[] separator = { ' ', '\r' };
                            
                            string receiver = line.Split(separator)[1];
                            string message = line.Substring(3 + receiver.Length + 1);
                            server.WhisperMessage(this, receiver, message);
                            continue;
                        }

                        if (line.StartsWith("/quit"))
                        {
                            Disconnect();
                            break;
                        }

                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            form.AppendMessage("[" + username + "]: " + line);
                            server.BroadcastMessage(this, line);
                        }
                    }
                    
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        // Read incoming data from client
        private string ReadFromClient()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inStream = new byte[8196];
                ms.Write(inStream, 0, networkStream.Read(inStream, 0, inStream.Length));
                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }

        // Write data to network stream
        public void SendToClient(string message)
        {            
            try
            {
                byte[] outStream = Encoding.ASCII.GetBytes(message + "\r");
                networkStream.Write(outStream, 0, outStream.Length);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Disconnect user
        public void Disconnect()
        {
            try
            {
                SendToClient("/disconnect");
                
                if (!string.IsNullOrEmpty(username))
                {
                    server.SendServerMessage(username + " has disconnected.");
                }

                listening = false;
                server.RemoveClient(this);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Check username availability
        public void VerifyUserCredentials(string line)
        {
            // Get username and password from line
            char[] sep1 = { ':' };
            char[] sep2 = { ' ', '\r' };
            string uname = line.Split(sep1)[0].Split(sep2)[1];
            string pword = line.Split(sep1)[1].Split(sep2)[1];

            if (server.VerifyUser(uname, pword))
            {
                username = uname;
                SendToClient("/uname 1");
                server.SendServerMessage(username + " has connected.");
            } else
            {
                SendToClient("/uname 0");
                Disconnect();
            }
        }
    }
}
