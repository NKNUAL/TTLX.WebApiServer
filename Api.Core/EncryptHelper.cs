using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core
{
    public class EncryptHelper
    {
        static string desKey = "ttlx.api";

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <returns></returns>
        public static string MD5Encrypt(string encryptString)
        {
            string returnValue;
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                returnValue = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(encryptString))).Replace("-", "");
                md5.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">密匙（8位）</param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptString)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider dES = new DESCryptoServiceProvider();
                byte[] byteEncrypt = Encoding.Default.GetBytes(encryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dES.CreateEncryptor(Encoding.Default.GetBytes(desKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncrypt, 0, byteEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Convert.ToBase64String(memoryStream.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密匙（8位）</param>
        /// <returns></returns>
        public static string DESDecrypt(string decryptString)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider dES = new DESCryptoServiceProvider();
                byte[] byteDecryptString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dES.CreateDecryptor(Encoding.Default.GetBytes(desKey), temp), CryptoStreamMode.Write);

                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);

                cryptoStream.FlushFinalBlock();

                returnValue = Encoding.Default.GetString(memoryStream.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;

        }
        /// <summary>
        /// RC2加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static string RC2Encrypt(string encryptString, string encryptKey)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                RC2CryptoServiceProvider rC2 = new RC2CryptoServiceProvider();
                byte[] byteEncryptString = Encoding.Default.GetBytes(encryptString);
                MemoryStream memorystream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memorystream, rC2.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncryptString, 0, byteEncryptString.Length);
                cryptoStream.FlushFinalBlock();

                returnValue = Convert.ToBase64String(memorystream.ToArray());



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;

        }
        /// <summary>
        /// RC2解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static string RC2Decrypt(string decryptString, string decryptKey)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                return "";
            }
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                RC2CryptoServiceProvider rC2 = new RC2CryptoServiceProvider();
                byte[] byteDecrytString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rC2.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecrytString, 0, byteDecrytString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Encoding.Default.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="encryptString">待加密密文</param>
        /// <param name="encryptKey1">密匙1(长度必须为8位)</param>
        /// <param name="encryptKey2">密匙2(长度必须为8位)</param>
        /// <param name="encryptKey3">密匙3(长度必须为8位)</param>
        /// <returns></returns>
        public static string DES3Encrypt(string encryptString, string encryptKey1, string encryptKey2, string encryptKey3)
        {

            string returnValue;
            try
            {
                returnValue = DESEncrypt(encryptString);
                returnValue = DESEncrypt(returnValue);
                returnValue = DESEncrypt(returnValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;

        }
        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey1">密匙1(长度必须为8位)</param>
        /// <param name="decryptKey2">密匙2(长度必须为8位)</param>
        /// <param name="decryptKey3">密匙3(长度必须为8位)</param>
        /// <returns></returns>
        public static string DES3Decrypt(string decryptString, string decryptKey1, string decryptKey2, string decryptKey3)
        {

            string returnValue;
            try
            {
                returnValue = DESDecrypt(decryptString);
                returnValue = DESDecrypt(returnValue);
                returnValue = DESDecrypt(returnValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">加密密匙</param>
        /// <returns></returns>
        public static string AESEncrypt(string encryptString, string encryptKey)
        {
            string returnValue;
            byte[] temp = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael AESProvider = Rijndael.Create();
            try
            {
                byte[] byteEncryptString = Encoding.Default.GetBytes(encryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, AESProvider.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncryptString, 0, byteEncryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Convert.ToBase64String(memoryStream.ToArray(), 0, memoryStream.ToArray().Length,
                                Base64FormattingOptions.InsertLineBreaks);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;

        }
        /// <summary>
        ///AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(string decryptString, string decryptKey)
        {
            string returnValue = "";
            byte[] temp = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael AESProvider = Rijndael.Create();
            try
            {
                byte[] byteDecryptString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, AESProvider.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Encoding.Default.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
    }
}
