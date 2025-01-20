using System.Security.Cryptography;
using System.Text;

namespace AdvanceC_FinalDemo.Security
{
    public class PasswordHasher
    {
        // Hash the password with the salt using SHA256
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

        // Verify the user's input password against the stored hashed password
        public static bool VerifyPassword(string password, string storedHashedPassword)
        {
            // Hash the input password with the stored salt
            string hashedInputPassword = HashedPassword(password);

            // Compare the hashed input password with the stored hashed password
            return hashedInputPassword == storedHashedPassword;
        }
    }
}