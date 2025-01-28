using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.File_Operations
{
    /// <summary>
    /// This class demonstrates how to use CryptoStream for file encryption and decryption 
    /// using the AES algorithm. It includes methods for encrypting and decrypting data 
    /// using a randomly generated key and initialization vector (IV).
    /// </summary>
    public class CryptoStreamExample
    {
        private readonly string filePath;

        public CryptoStreamExample(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// Encrypts data using the AES encryption algorithm and writes the encrypted data 
        /// to a file using a CryptoStream.
        /// </summary>
        /// <param name="plainText">The plaintext data to encrypt</param>
        public void EncryptData(string plainText)
        {
            try
            {
                using (Aes aesAlg = Aes.Create()) // Create AES algorithm object
                {
                    byte[] key = aesAlg.Key; // Get AES key
                    byte[] iv = aesAlg.IV; // Get AES initialization vector (IV)

                    // Open the file to write the encrypted data
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    using (CryptoStream cryptoStream = new CryptoStream(fs, aesAlg.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(plainText); // Convert plaintext to bytes
                        cryptoStream.Write(data, 0, data.Length); // Encrypt and write to file
                        cryptoStream.FlushFinalBlock(); // Ensure all data is written to the file
                        Console.WriteLine("Data encrypted and written to file.");
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Encryption error: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("File error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Decrypts data from a file using the AES decryption algorithm and prints the 
        /// decrypted data to the console.
        /// </summary>
        public void DecryptData()
        {
            try
            {
                using (Aes aesAlg = Aes.Create()) // Create AES algorithm object
                {
                    byte[] key = aesAlg.Key; // Get AES key
                    byte[] iv = aesAlg.IV; // Get AES initialization vector (IV)

                    // Open the file to read the encrypted data
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (CryptoStream cryptoStream = new CryptoStream(fs, aesAlg.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[fs.Length]; // Buffer to hold decrypted data
                        cryptoStream.Read(buffer, 0, buffer.Length); // Decrypt data from the file
                        string decryptedContent = Encoding.UTF8.GetString(buffer); // Convert decrypted bytes to string
                        Console.WriteLine("Decrypted data: " + decryptedContent);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Decryption error: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("File error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
