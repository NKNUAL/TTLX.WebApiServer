using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class PaperQueryServiceModel
    {
        public string SchoolNo { get; set; }
        public string SpecialtyId { get; set; }
        /// <summary>
        /// 出题人
        /// </summary>
        public string UseToken { get; set; }
        public int? PaperStatu { get; set; }
        public int? CheckStatu { get; set; }
        public int OrderType { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// 购买人
        /// </summary>
        public string BoughtUserToken { get; set; }
    }
}
