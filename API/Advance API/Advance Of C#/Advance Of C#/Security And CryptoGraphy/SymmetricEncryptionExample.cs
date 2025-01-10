using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    public class SymmetricEncryptionExample
    {
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

        // Encrypts the plain text using AES with the provided key and IV
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

        // Decrypts the cipher text using AES with the provided key and IV
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
