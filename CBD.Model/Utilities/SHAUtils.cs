using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CBD.Model.Utilities
{
    public static class SHAUtils
    {
        private const string KEY_ARRAY_DEFAULT = "BICC-CBD";
        public static string ToSHAPassword(this string text)
        {
            text = text + Encode(text, KEY_ARRAY_DEFAULT);
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }

        public static string DeSHAPassword(this string text)
        {
            var password = Decode(text, KEY_ARRAY_DEFAULT);
            //byte[] bytes = Encoding.Unicode.GetBytes(text);
            //SHA256Managed hashstring = new SHA256Managed();
            //byte[] hash = hashstring.ComputeHash(bytes);
            //string hashString = string.Empty;
            //foreach (byte x in hash)
            //{
            //    hashString += String.Format("{0:x2}", x);
            //}

            return password;
        }

        public static string Encode(this string toEncrypt, string key)
        {
            if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(toEncrypt)) return string.Empty;
            byte[] preKeyArray = KEY_ARRAY_DEFAULT.ToCharArray().Select(x => (byte)x).ToArray();
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(KEY_ARRAY_DEFAULT + key));
            Array.Resize<byte>(ref preKeyArray, keyArray.Length);
            keyArray.CopyTo(preKeyArray, preKeyArray.Length - keyArray.Length);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = preKeyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Decode 2 dimentions
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decode(this string toDecrypt, string key)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
                byte[] preKeyArray = KEY_ARRAY_DEFAULT.ToCharArray().Select(x => (byte)x).ToArray();

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(KEY_ARRAY_DEFAULT + key));
                Array.Resize<byte>(ref preKeyArray, keyArray.Length);
                keyArray.CopyTo(preKeyArray, preKeyArray.Length - keyArray.Length);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = preKeyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                if (toEncryptArray.Length == 0) return string.Empty;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
