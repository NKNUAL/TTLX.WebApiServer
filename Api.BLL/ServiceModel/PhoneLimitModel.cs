using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class PhoneLimitModel
    {
        public string SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }

        public int LimitCount { get; set; }
    }
}
