using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IAM.Common
{
    public class Encryption
    {
        static string PlainPattern = "{0}:{1}"; //{Key}:{plainText}
        static readonly string KEY = ConfigurationManager.AppSettings["publicKey"];
        public static string Encrypt(string plainText)
        {
            var text = string.Format(PlainPattern, KEY, plainText);
            return EncryptString(KEY, text);
        }

        public static string Decrypt(string cipherText)
        {
            var textPattern = DecryptString(KEY, cipherText);
            var text = textPattern.Split(':')[1];
            return text;
        }

        private static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        private static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new MemoryStream(buffer);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }


    }
}
