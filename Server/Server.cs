using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    class Server
    {
        public List<ClientHandler> clients = new List<ClientHandler>();
        private List<User> users = new List<User>();
        private Form form;
        public TcpListener serverSocket;
        public int port = 3001;
        public string Status { get; set; }
        private bool listening;

        public Server(Form form)
        {
            this.form = form;
            Status = "OFFLINE";
        }

        // Starts the server
        public void StartServer()
        {
            try
            {
                LoadUsers();
                IPAddress[] ipAddresses = Dns.GetHostAddresses("192.168.20.2");
                IPAddress ip = ipAddresses[0];
                serverSocket = new TcpListener(ip, port);
                serverSocket.Start();
                TcpClient clientSocket = default(TcpClient);

                Status = "ONLINE";
                form.AppendMessage("Server started. Listening on port: " + port);

                listening = true;
                while (listening == true)
                {
                    clientSocket = serverSocket.AcceptTcpClient();
                    ClientHandler client = new ClientHandler(form, this);
                    client.Connect(clientSocket);
                    clients.Add(client);
                }
                clientSocket = null;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Stops the server
        public void StopServer()
        {
            SaveUsers();

            try
            {
                DisconnectAll();
                listening = false;
                serverSocket.Stop();
                Status = "OFFLINE";
                form.AppendMessage("Server stopped.");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Remove client from client list
        public void RemoveClient(ClientHandler client)
        {
            clients.Remove(client);
        }

        // Add a new user
        public void AddUser(string username, string password)
        {
            if (users.Find(x => x.username == username) == null)
            {
                User user = new User(username, password);
                users.Add(user);
                DisplayStatusMessage("User added: " + username);
            }
            else
            {
                DisplayStatusMessage("User already exists");
            }
        }

        // Remove a user
        public void RemoveUser(string username)
        {
            User user = users.Find(x => x.username == username);
            ClientHandler client = clients.Find(x => x.username == username);

            if (client != null)
            {
                client.Disconnect();
            }
            
            if (users.Remove(user))
            {
                DisplayStatusMessage("User removed: " + username);
            } else
            {
                DisplayStatusMessage("User not found.");
            }
        }

        // Reset user password
        public void ResetUserPassword (string username, string password)
        {
            if (users.Find(x => x.username == username).ResetPassword(password))
            {
                DisplayStatusMessage(username + "'s password has been reset.");
            } else
            {
                DisplayStatusMessage("Unable to reset password for user: '" + username + "'");
            } 
            
        }

        // Disconnect all clients
        public void DisconnectAll()
        {
            foreach (ClientHandler client in clients)
            {
                try
                {
                    client.Disconnect();
                }
                catch (IOException)
                {

                }
            }
        }

        // Display all current users
        public void DisplayUsers()
        {
            if (users.Count == 0)
            {
                DisplayStatusMessage("No users registered.");
            }
            users.ForEach(x => DisplayStatusMessage(x.username));
        }

        // Store user information in file
        private void SaveUsers()
        {
            FileStream fs = new FileStream("users.bin", FileMode.Create);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, users);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine(ex.Message);
                DisplayStatusMessage("Unable to save users to file.");
            } finally
            {
                fs.Close();
                DisplayStatusMessage("Users saved.");
            }
        }

        // Load user information from file
        private void LoadUsers()
        {
            FileStream fs = new FileStream("users.bin", FileMode.OpenOrCreate);
            
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                users = (List<User>)formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                DisplayStatusMessage("Unable to load users from file.");
            } finally
            {
                fs.Close();
                DisplayStatusMessage("Users loaded.");
            }
        }

        // Verify user
        public bool VerifyUser(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.username.Equals(username))
                {
                    if (user.VerifyPassword(password))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        // Region contains all methods used to communicate between clients
        #region Messaging methods

        // Display server status message
        private void DisplayStatusMessage(string message)
        {
            form.AppendMessage(message);
        }

        // Send system (server) message to all users
        public void SendServerMessage(string message)
        {
            foreach (ClientHandler client in clients)
            {
                client.SendToClient("[#Server]: " + message);
            }
            form.AppendMessage("[#Server]: " + message);
        }

        // Broadcast message to all users except sender
        public void BroadcastMessage(ClientHandler sender, string message)
        {
            foreach (ClientHandler client in clients)
            {
                if (client != sender)
                {
                    client.SendToClient("[" + sender.username + "]: " + message);
                }
            }
        }

        // Send message to specified client
        public void WhisperMessage(ClientHandler sender, string receiver, string message)
        {
            foreach (ClientHandler client in clients)
            {
                if (client.username.Equals(receiver))
                {
                    client.SendToClient("/w [" + sender.username + "] whispers: " + message);
                    return;
                }
            }
            sender.SendToClient("* Unable to send whisper to '" + receiver + "'.");
        }

        #endregion
    }
}
