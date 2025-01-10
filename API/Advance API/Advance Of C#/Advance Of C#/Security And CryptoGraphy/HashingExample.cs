using System;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    public class HashingExample
    {
        public string ComputeHash(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach(byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
