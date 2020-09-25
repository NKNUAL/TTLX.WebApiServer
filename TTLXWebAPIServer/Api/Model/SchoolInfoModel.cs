using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class SchoolInfoModel
    {
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string SchoolGpCode { get; set; }
        public string FK_Province { get; set; }
    }
}