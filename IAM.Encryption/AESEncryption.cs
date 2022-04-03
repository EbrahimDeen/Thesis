using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace IAM.Encryption
{
    public class AESEncryption
    {
        Aes myAes;

        public static byte[] Encrypt(string plainText)
        {
            using Aes myAes = Aes.Create();
            int sizeInKb = 2;
            byte[] IV = new byte[sizeInKb * 1024];
            new RNGCryptoServiceProvider().GetBytes(IV);
            var encryptes = EncryptStringToBytes_Aes(plainText, myAes.Key, IV);
            return encryptes;
        }
        public static string Decrypt(byte[] cipher)
        {
            using Aes myAes = Aes.Create();
            int sizeInKb = 2;
            byte[] IV = new byte[sizeInKb * 1024];
            //new RNGCryptoServiceProvider().GetBytes(IV);
            var decryptes = DecryptStringFromBytes_Aes(cipher, myAes.Key, IV);
            return decryptes;
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
            byte[] withKey = encrypted.Concat(Key).Concat(IV).ToArray();
            // Return the encrypted bytes from the memory stream.
            return withKey;
        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {

            var dataLength = cipherText.Length - Key.Length - IV.Length;
            byte[] data = new byte[dataLength];
            Array.Copy(cipherText, data, dataLength);

            Array.Copy(cipherText, data.Length, Key, 0, Key.Length);
            Array.Copy(cipherText, data.Length + Key.Length, IV, 0, IV.Length);

            // Check arguments.
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using MemoryStream msDecrypt = new MemoryStream(data);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                // Read the decrypted bytes from the decrypting stream
                // and place them in a string.
                plaintext = srDecrypt.ReadToEnd();
            }

            return plaintext;
        }
    }
}
