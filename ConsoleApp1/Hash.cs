using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public class Hash
    {
        private static string oauth_secrate = "2ebea3f0-78dd-46be-abe0-192ee9e69335";

        public static string GenarateSignature(string timestamp, string URL, string Json)
        {
            string data = timestamp + URL + Json;

            // Initialize the keyed hash object using the secret key as the key
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(oauth_secrate));

            // Computes the signature by hashing the salt with the secret key as the key
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Base 64 Encode
            return Convert.ToBase64String(signature);

        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
