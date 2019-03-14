using System;
using System.Security.Cryptography;
using System.Text;

namespace Pluto.Utils.Cryptography
{
    public static class Cryptography
    {
        public static string Encrypt(this string value)
        {
            var valueArray = Encoding.UTF8.GetBytes(value);

            using (var provider = GetProvider())
            {
                var encryptor = provider.CreateEncryptor();
                var resultArray = encryptor.TransformFinalBlock(valueArray, 0, valueArray.Length);
                var result = Convert.ToBase64String(resultArray);

                return result;
            }
        }

        public static string Decrypt(this string value)
        {
            var valueArray = Encoding.UTF8.GetBytes(value);

            using (var provider = GetProvider())
            {
                var decryptor = provider.CreateDecryptor();
                var resultArray = decryptor.TransformFinalBlock(valueArray, 0, valueArray.Length);
                var result = Convert.ToBase64String(resultArray);

                return result;
            }
        }

        private static byte[] GetKeyHash()
        {
            const string key = "Sp4k1";
            using (var provider = new MD5CryptoServiceProvider())
            {
                return provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }

        private static TripleDESCryptoServiceProvider GetProvider()
        {
            var result = new TripleDESCryptoServiceProvider();
            result.Key = GetKeyHash();
            result.Mode = CipherMode.ECB;
            result.Padding = PaddingMode.PKCS7;

            return result;
        }
    }
}
