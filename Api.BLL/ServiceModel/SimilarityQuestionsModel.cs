using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class SimilarityQuestionsModel
    {
        public string QueContent { get; set; }
        public string Option0 { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public double Similarity { get; set; }
    }
}
