using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Enum
{
    public enum AliPayStatu
    {
        /// <summary>
        /// 未付款交易关闭
        /// </summary>
        Closed,
        /// <summary>
        /// 已付款
        /// </summary>
        Paid,
        /// <summary>
        /// 等待付款
        /// </summary>
        WaitingPay,
        /// <summary>
        /// 未知
        /// </summary>
        Nono,
    }
}
