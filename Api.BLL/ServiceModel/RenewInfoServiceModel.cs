using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class RenewInfoServiceModel
    {
        public string RenewNo { get; set; }
        public string QrCode { get; set; }
        public Dictionary<string, double> SpecialtyPrice { get; set; }
        public string Explan { get; set; }
    }
}
