using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class RenewCheckModel
    {
        public string RenewNo { get; set; }
        public string CPUID { get; set; }
        public string DISKID { get; set; }
        public string UUID { get; set; }
    }
    public class LicenseModel
    {
        public string SchoolNo { get; set; }
        public string CPUID { get; set; }
        public string DISKID { get; set; }
        public string UUID { get; set; }
    }
}