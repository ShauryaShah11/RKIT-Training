using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.File_Operations
{
    public class CryptoStreamExample
    {
        private readonly string filePath;

        public CryptoStreamExample(string path)
        {
            filePath = path;
        }

        // Encrypt data using CryptoStream
        public void EncryptData(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = aesAlg.Key;
                byte[] iv = aesAlg.IV;

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(fs, aesAlg.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] data = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock(); // Ensure all data is written
                    Console.WriteLine("Data encrypted and written to file.");
                }
            }
        }

        // Decrypt data using CryptoStream
        public void DecryptData()
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = aesAlg.Key;
                byte[] iv = aesAlg.IV;

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (CryptoStream cryptoStream = new CryptoStream(fs, aesAlg.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    cryptoStream.Read(buffer, 0, buffer.Length);
                    string decryptedContent = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine("Decrypted data: " + decryptedContent);
                }
            }
        }
    }
}
