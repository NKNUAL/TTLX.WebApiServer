using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class BindUserModel
    {

        public string UserToken { get; set; }
        public string UserName { get; set; }
        public string SchoolNo { get; set; }
        public string SchoolName { get; set; }
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }

    }
}
