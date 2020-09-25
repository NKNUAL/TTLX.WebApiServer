using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Enum
{
    public enum BindType
    {

        NotBind = 0,//未绑定
        Pass = 1,//绑定成功
        Baned = 2,//禁用
        Nono = -1//未知
    }
}
