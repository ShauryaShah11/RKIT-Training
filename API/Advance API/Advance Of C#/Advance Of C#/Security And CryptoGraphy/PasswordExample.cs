using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    public  class PasswordExample
    {
        // Generate a random salt
        public byte[] GenerateSalt(int size = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }

        // Hash the password with the salt using SHA256
        public string HashedPassword(string password, byte[] salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Combine password and salt
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, passwordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, salt.Length, passwordBytes.Length);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordWithSalt);

                // Convert hash to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // Verify the user's input password against the stored hashed password
        public bool VerifyPassword(string inputPassword, byte[] storedSalt, string storedHashedPassword)
        {
            // Hash the input password with the stored salt
            string hashedInputPassword = HashedPassword(inputPassword, storedSalt);

            // Compare the hashed input password with the stored hashed password
            return hashedInputPassword == storedHashedPassword;
        }
    }
}
