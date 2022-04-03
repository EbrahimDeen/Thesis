using IAM.Encryption;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IAM.Common
{
    public class Encryption
    {
        public static byte[] Encrypt(string plainText)
        {
            return AESEncryption.Encrypt(plainText);
        }

        public static string Decrypt(byte[] cipherText)
        {
            return AESEncryption.Decrypt(cipherText);   
        }
    }
}
