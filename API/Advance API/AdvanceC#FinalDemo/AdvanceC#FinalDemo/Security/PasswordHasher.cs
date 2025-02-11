using System.Security.Cryptography;
using System.Text;

namespace AdvanceC_FinalDemo.Security
{
    /// <summary>
    /// Provides password hashing and verification functionality using SHA256.
    /// Note: For production systems, consider using more secure methods like PBKDF2, Bcrypt, or Argon2.
    /// </summary>
    public class PasswordHasher
    {
        /// <summary>
        /// Computes the SHA256 hash of a given password.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>A hexadecimal string representation of the hashed password.</returns>
        /// <remarks>
        /// The method uses SHA256 to create a one-way hash of the password.
        /// Each byte of the hash is converted to a two-character hexadecimal string.
        /// </remarks>
        public static string HashedPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifies if an input password matches a stored hashed password.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="storedHashedPassword">The previously hashed password to compare against.</param>
        /// <returns>True if the passwords match, false otherwise.</returns>
        /// <remarks>
        /// The method hashes the input password and performs a time-constant comparison
        /// with the stored hash to prevent timing attacks.
        /// </remarks>
        public static bool VerifyPassword(string password, string storedHashedPassword)
        {
            // Hash the input password with the stored salt
            string hashedInputPassword = HashedPassword(password);
            // Compare the hashed input password with the stored hashed password
            return hashedInputPassword == storedHashedPassword;
        }
    }
}