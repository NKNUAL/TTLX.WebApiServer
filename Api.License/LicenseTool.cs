using Api.Core.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TTLX.License;

namespace Api.License
{
    public class LicenseTool
    {

        /// <summary>
        /// 生成授权码
        /// </summary>
        /// <returns></returns>
        public static string GenerateAuthCode(string cup_id, string disk_id, string uuid)
        {
            if (string.IsNullOrEmpty(cup_id) ||
                string.IsNullOrEmpty(disk_id) ||
                string.IsNullOrEmpty(uuid))
            {
                throw new Exception("参数异常，参数不能为空或null");
            }

            return TTLX.License.Encoder.DESEncrypt(cup_id + disk_id + uuid);
        }

        /// <summary>
        /// 生成授权文件,如果isSetExpire未true，则expire可以不设置
        /// </summary>
        /// <param name="cup_id"></param>
        /// <param name="disk_id"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static byte[] GenerateLicense(string cup_id, string disk_id, string uuid,
            DateTime expire, bool isSetExpire = true)
        {
            string crypt_file = System.Web.Hosting.HostingEnvironment
                .MapPath(@"~/App_Data/ttlxexam.pfx");
            if (!File.Exists(crypt_file))
            {
                throw new Exception("加密私匙文件不存在！");
            }

            string authCode = GenerateAuthCode(cup_id, disk_id, uuid);
            LicenseData data = new LicenseData();
            data.AddData(LicenseDataType.Mac, new string[] { authCode });

            if (isSetExpire)
            {
                if ((expire.Date - DateTime.Now.Date).Days <= 0)
                {
                    throw new Exception("过期日期不能小于或等于当前日期");
                }

                // 避免因时区调整不当引起实使用时还不到开始时间的问题
                var date = (DateTime.Now.Date - new TimeSpan(3, 0, 0, 0));
                data.AddData(LicenseDataType.StartDate, date.Date.ToUniversalTime().Ticks);

                // 将截止时间转换为UTC时间
                DateTime endDate = expire.Date.ToUniversalTime();

                data.AddData(LicenseDataType.EndDate, endDate.Ticks);
            }

            // 生成license文件
            try
            {
                // 这个日期在第一次使用时会更新成实际使用的日期和时间
                // 纠正时区使用不当引起的日期问题
                var now = (DateTime.Now - new TimeSpan(3, 0, 0, 0));
                data.AddData(LicenseDataType.CurrentDate, now.ToUniversalTime().Ticks);
                string temp = "123456";
                byte[] rawData = data.GetStillData(crypt_file, temp, true);
                byte[] destByte;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bs = new BinaryWriter(ms))
                    {
                        // 写动态数据
                        byte[] dynamicData = data.GetDynamicData();
                        bs.Write(dynamicData, 0, dynamicData.Length);

                        // 写静态签名数据
                        bs.Seek(100, SeekOrigin.Begin);
                        bs.Write(rawData, 0, rawData.Length);
                    }
                    destByte = ms.GetBuffer();
                }
                return destByte;
            }
            catch (Exception ex)
            {
                throw new Exception("创建license出错：" + ex.Message);
            }

        }
    }
}
