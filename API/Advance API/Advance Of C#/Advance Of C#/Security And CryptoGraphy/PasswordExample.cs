using System;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    /// <summary>
    /// This class demonstrates how to securely handle passwords by generating a salt, hashing a password with the salt,
    /// and verifying a password by comparing it with a stored hashed password.
    /// It uses SHA-256 for hashing and incorporates the use of salt to strengthen the password hash and protect against rainbow table attacks.
    /// </summary>
    public class PasswordExample
    {
        /// <summary>
        /// Generates a random salt, which is a unique value used to add randomness to password hashing.
        /// The salt helps prevent attackers from using precomputed hashes (rainbow tables) to crack passwords.
        /// </summary>
        /// <param name="size">The size of the salt in bytes (default is 32 bytes).</param>
        /// <returns>A byte array representing the generated salt.</returns>
        public byte[] GenerateSalt(int size = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Hashes the password by combining it with a salt and using SHA-256 hashing.
        /// This ensures that the password is securely stored by adding an additional layer of protection through the salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to combine with the password before hashing.</param>
        /// <returns>A string representing the hashed password in hexadecimal format.</returns>
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

        /// <summary>
        /// Verifies whether the user's input password matches the stored password hash.
        /// The input password is hashed with the stored salt and compared with the stored hashed password.
        /// </summary>
        /// <param name="inputPassword">The password entered by the user to verify.</param>
        /// <param name="storedSalt">The salt used when the password was initially hashed.</param>
        /// <param name="storedHashedPassword">The previously stored hashed password.</param>
        /// <returns>True if the input password matches the stored hashed password; otherwise, false.</returns>
        public bool VerifyPassword(string inputPassword, byte[] storedSalt, string storedHashedPassword)
        {
            // Hash the input password with the stored salt
            string hashedInputPassword = HashedPassword(inputPassword, storedSalt);

            // Compare the hashed input password with the stored hashed password
            return hashedInputPassword == storedHashedPassword;
        }
    }
}
