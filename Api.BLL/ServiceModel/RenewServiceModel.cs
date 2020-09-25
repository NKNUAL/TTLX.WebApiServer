using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class RenewServiceModel
    {
        public int RenewType { get; set; }
        public List<string> SpecialtyIds { get; set; }
        public string SchoolNo { get; set; }
    }
}
