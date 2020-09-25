using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class RenewCheckServiceModel
    {
        public string RenewNo { get; set; }
        public string CPUID { get; set; }
        public string DISKID { get; set; }
        public string UUID { get; set; }
    }

    public class LicenseServiceModel
    {
        public string SchoolNo { get; set; }
        public string CPUID { get; set; }
        public string DISKID { get; set; }
        public string UUID { get; set; }
    }
}
