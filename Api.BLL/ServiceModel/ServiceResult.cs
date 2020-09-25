using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class ServiceResult
    {
        public bool success { get; set; }

        public string message { get; set; }

        public dynamic data { get; set; }
    }
}
