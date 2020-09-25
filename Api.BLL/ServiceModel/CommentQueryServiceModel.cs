using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class CommentQueryServiceModel
    {
        public string PaperID { get; set; }
        public int OrderType { get; set; }
        public string OrderBy { get; set; }
    }
}
