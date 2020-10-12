using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Api.Core
{
    public class ConfigTools
    {
        static readonly string desKey = "ttlx.@pd";
        /// <summary>
        /// 获取数据库连接字符串（带解密）
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetDBConnString(string configName)
        {
            string connStr = ConfigurationManager.ConnectionStrings[configName].ConnectionString;


            string ipFlag = "data source=";
            int ipIndex = connStr.IndexOf(ipFlag) + ipFlag.Length;
            int ipEendIndex = connStr.IndexOf(";");
            string ipCipher = connStr.Substring(ipIndex, ipEendIndex - ipIndex);
            string ip = DESDecrypt(ipCipher);

            // 取用户ID
            string flag = "user id=";
            int index = connStr.IndexOf(flag) + flag.Length;
            int endIndex = connStr.LastIndexOf(";");
            string userCipher = connStr.Substring(index, endIndex - index);
            string userid = DESDecrypt(userCipher);

            // 取密码
            string pwdFlag = "password=";
            int pwdIndex = connStr.IndexOf(pwdFlag) + pwdFlag.Length;
            string pwdCipher = connStr.Substring(pwdIndex);
            string pwd = DESDecrypt(pwdCipher);

            // 转换成明文连接字符串
            connStr = connStr.Replace(ipCipher, ip);
            connStr = connStr.Replace(userCipher, userid);
            connStr = connStr.Replace(pwdCipher, pwd);

            return connStr;
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

    }
}