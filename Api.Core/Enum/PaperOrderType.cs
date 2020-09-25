using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Enum
{
    public enum PaperOrderType
    {
        Score = 0,//好评度
        Price = 1,//价格
        NumberOfPurchasers = 2,//采购人数
        CreateDate = 3,//出题时间
    }
}
