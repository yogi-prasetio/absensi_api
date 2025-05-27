using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace absensi_api.Helper
{
    public class EncryptionHelper
    {
        private static byte[] GetKeyBytes(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key)); // 32 bytes (256-bit)
            }
        }

        public static string EncryptString(string plainText, IConfiguration config)
        {
            var key = config["Jwt:Key"];
            byte[] keyBytes = GetKeyBytes(key);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.GenerateIV(); // Use random IV
                byte[] iv = aesAlg.IV;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(iv, 0, iv.Length); // Prepend IV

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText, IConfiguration config)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            var key = config["Jwt:Key"];
            byte[] keyBytes = GetKeyBytes(key);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;

                byte[] iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aesAlg.IV = iv;

                using (MemoryStream msDecrypt = new MemoryStream(fullCipher, 16, fullCipher.Length - 16))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        // public static Dictionary<string, object> DecryptDictionary(Dictionary<string, object> encryptedDict, IConfiguration config)
        // {
        //     // Created a empty dictionary to store the decrypted value
        //     var decryptedDict = new Dictionary<string, object>();
        //     // Loop through the dictionary and decrypt the value if it is a string
        //     foreach (var kvp in encryptedDict)
        //     {
        //         // Check if the value is a string
        //         if (kvp.Value is string encryptedString)
        //         {
        //             // Try to decrypt all the content of dictionary
        //             try
        //             {
        //                 // Decrypt the encrypted data then store it to the previous empty dictionary
        //                 var decryptedValue = DecryptString(encryptedString, config);
        //                 decryptedDict[kvp.Key] = decryptedValue;
        //             }
        //             catch (Exception)
        //             {
        //                 if (kvp.Key == "candidateTestId" || kvp.Key == "candidateId" || kvp.Key == "testId")
        //                 {
        //                     //throw new Exception("Unauthorized data modification");
        //                     // Custom exception for returning 401 instead of 500 with custom messge
        //                     throw ExceptionHelper.CustomException(401, "Unauthorized data modification");
        //                 }
        //                 else
        //                 {
        //                     // If the data fail to decrypt, then its not encrypted, so just return the original value
        //                     decryptedDict[kvp.Key] = encryptedString;
        //                 }
        //             }
        //         }
        //         // If the value is not a string, then just return the original value
        //         else
        //         {
        //             decryptedDict[kvp.Key] = kvp.Value;
        //         }
        //     }
        //     return decryptedDict;
        // }
    }
}
