using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL
{
    public interface IVerifyService : IDependency
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="schoolNo"></param>
        /// <param name="gpCode"></param>
        /// <returns></returns>
        bool AuthVerify(string schoolNo, string gpCode);
    }
}
