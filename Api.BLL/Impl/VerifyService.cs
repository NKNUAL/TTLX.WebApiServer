using Api.Core.Redis;
using Api.DAL;
using Api.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.Impl
{
    public class VerifyService : IVerifyService
    {
        private readonly DbServer0905Context _dbServer0905 = DbContextFactory.GetDbServer0905Context();


        public bool AuthVerify(string schoolNo, string gpCode)
        {
            if (string.IsNullOrEmpty(schoolNo) || string.IsNullOrEmpty(gpCode))
                return false;

            var redis = new RedisHelper();

            string key = RedisKeys.GpCodeKey;

            if (redis.KeyExists(key))
            {
                if (redis.HashExists(key, schoolNo))
                {
                    return gpCode.Equals(redis.HashGet<string>(key, schoolNo));
                }
            }

            var school = _dbServer0905.Base_School.FirstOrDefault(s => s.SchoolNo == schoolNo);

            if (string.IsNullOrEmpty(school.GPCode))
                return false;

            redis.HashSet(key, school.SchoolNo, school.GPCode);

            return gpCode.Equals(school.GPCode);

        }
    }
}
