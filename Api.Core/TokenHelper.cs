using Api.Core.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Core
{
    public class TokenHelper
    {
        private static readonly Lazy<TokenHelper> _instance = new Lazy<TokenHelper>(() => new TokenHelper());
        public static TokenHelper Instance
        {
            get { return _instance.Value; }
        }

        /// <summary>
        /// 生成模拟试卷的Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        public string GenerateMockToken(string userId, string pwd, string specialtyId)
        {
            string key = string.Format(RedisKeys.MockToken, userId);

            string dataKey = DateTime.Now.ToString("yyyyMMddHHmmss");

            string value = $"{userId}\\{pwd}\\{specialtyId}\\{dataKey}";

            string des_value = EncryptHelper
                .DESEncrypt(value);

            new RedisHelper().HashSet(key, dataKey, des_value);

            return des_value;
        }


        public bool TokenVerify(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            try
            {
                string value = EncryptHelper.DESDecrypt(token);
                var items = value.Split('\\');
                //判断redis中是否存在token即可
                RedisHelper redis = new RedisHelper();

                string key = string.Format(RedisKeys.MockToken, items[0]);

                if (redis.KeyExists(key))
                {
                    string dataKey = items[3];
                    return redis.HashExists(key, dataKey);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
