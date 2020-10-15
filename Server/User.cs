using System;
using System.Security.Cryptography;
using System.Text;

namespace Server
{
    [Serializable]
    class User
    {
        public string username { get; set; }
        public string salt { get; set; }
        private string passwordHash;

        public User(string username, string password)
        {
            this.username = username;
            salt = GenerateSalt();
            passwordHash = HashPassword(password, salt);
        }

        // Generate password hash
        private string HashPassword(string password, string salt)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            string passwordString = password + salt;
            byte[] dataBytes = Encoding.ASCII.GetBytes(passwordString);
            return Encoding.ASCII.GetString(sha.ComputeHash(dataBytes));
        }

        // Generate password salt
        private string GenerateSalt()
        {
            int saltSize = 24;
            RNGCryptoServiceProvider m_CryptoServiceProvider = new RNGCryptoServiceProvider();

            byte[] saltBytes = new byte[saltSize];
            m_CryptoServiceProvider.GetNonZeroBytes(saltBytes);

            return Encoding.ASCII.GetString(saltBytes);
        }

        // Verify password
        public bool VerifyPassword(string password)
        {
            return passwordHash == HashPassword(password, salt);
        }

        // Reset password
        public bool ResetPassword(string password)
        {
            passwordHash = HashPassword(password, salt);
            return true;
        }
    }
}