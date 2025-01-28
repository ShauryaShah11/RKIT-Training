using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    /// <summary>
    /// This class demonstrates how to compute a hash for a given password using the SHA-256 algorithm.
    /// It securely transforms a password into a fixed-length string (hash) that can be stored or verified.
    /// The process involves creating a SHA256 object, computing the hash, and converting it into a readable hexadecimal format.
    /// </summary>
    public class HashingExample
    {
        /// <summary>
        /// Computes the SHA-256 hash of the provided password.
        /// The method converts the password string into a byte array, computes the hash,
        /// and then returns the hash as a hexadecimal string.
        /// </summary>
        /// <param name="password">The password string that needs to be hashed.</param>
        /// <returns>A hexadecimal string representing the SHA-256 hash of the password.</returns>
        public string ComputeHash(string password)
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
    }
}
