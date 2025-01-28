using System;
using System.IO;
using System.Security.Cryptography;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    /// <summary>
    /// This class demonstrates how to perform symmetric encryption and decryption using the AES algorithm.
    /// It provides methods to generate a random encryption key and initialization vector (IV), encrypt plain text,
    /// and decrypt cipher text using AES (Advanced Encryption Standard).
    /// The key and IV are handled as Base64-encoded strings for easy storage and transmission.
    /// </summary>
    public class SymmetricEncryptionExample
    {
        /// <summary>
        /// Generates a random AES encryption key and returns it as a Base64-encoded string.
        /// The key is used in both encryption and decryption to secure the data.
        /// </summary>
        /// <returns>A Base64-encoded string representing the AES encryption key.</returns>
        public string GetKey()
        {
            string key;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey(); // Generate a random key
                key = Convert.ToBase64String(aesAlg.Key); // Convert key to Base64 string
            }
            return key;
        }

        /// <summary>
        /// Generates a random AES initialization vector (IV) and returns it as a Base64-encoded string.
        /// The IV adds randomness to the encryption process and is used along with the key in both encryption and decryption.
        /// </summary>
        /// <returns>A Base64-encoded string representing the AES IV.</returns>
        public string GetIV()
        {
            string iv;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV(); // Generate a random IV
                iv = Convert.ToBase64String(aesAlg.IV); // Convert IV to Base64 string
            }
            return iv;
        }

        /// <summary>
        /// Encrypts the given plain text using AES with the specified key and IV.
        /// The method converts the plain text into encrypted cipher text and returns it as a Base64 string.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="key">The AES encryption key (Base64-encoded).</param>
        /// <param name="iv">The AES initialization vector (Base64-encoded).</param>
        /// <returns>A Base64-encoded string representing the encrypted cipher text.</returns>
        public string Encrypt(string plainText, string key, string iv)
        {
            // Decode the key and IV from Base64 to byte arrays
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes; // Set the key
                aesAlg.IV = ivBytes;   // Set the IV

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(csEncrypt))
                        {
                            sw.Write(plainText); // Write plain text to the stream, which will be encrypted
                        }
                    }

                    // Return the encrypted text as a Base64 string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts the given cipher text using AES with the specified key and IV.
        /// The method converts the cipher text back into plain text and returns it as a string.
        /// </summary>
        /// <param name="cipherText">The encrypted cipher text to decrypt (Base64-encoded).</param>
        /// <param name="key">The AES encryption key (Base64-encoded).</param>
        /// <param name="iv">The AES initialization vector (Base64-encoded).</param>
        /// <returns>The decrypted plain text.</returns>
        public string Decrypt(string cipherText, string key, string iv)
        {
            // Decode the key and IV from Base64 to byte arrays
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes; // Set the key
                aesAlg.IV = ivBytes;   // Set the IV

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Convert the cipher text (Base64 string) back to byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd(); // Return the decrypted plain text
                        }
                    }
                }
            }
        }
    }
}
