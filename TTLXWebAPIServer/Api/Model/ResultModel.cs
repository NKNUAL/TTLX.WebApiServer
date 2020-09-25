using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class HttpResultModel
    {
        public string Ret_status { get; set; }

        public string Ret_message { get; set; }

        public dynamic data { get; set; }
    }
}