using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api
{
    public class HttpResultModel
    {
        public bool success { get; set; }

        public string message { get; set; }

        public dynamic data { get; set; }
    }

    public class HttpResultByteModel
    {
        public bool success { get; set; }

        public string message { get; set; }

        public byte[] data { get; set; }
    }
}