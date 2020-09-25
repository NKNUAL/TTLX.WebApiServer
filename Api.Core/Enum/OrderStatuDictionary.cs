using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Enum
{
    public enum OrderStatuDictionary
    {
        /// <summary>
        /// 预创建
        /// </summary>
        PreCreated = 0,
        /// <summary>
        /// 完成
        /// </summary>
        Finished = 1,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed = 2,
        /// <summary>
        /// 退款
        /// </summary>
        BackMoney = 3,
    }
}
