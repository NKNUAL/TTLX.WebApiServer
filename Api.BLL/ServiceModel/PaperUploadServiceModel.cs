using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class PaperUploadServiceModel
    {

        public string UserToken { get; set; }

        public string PaperName { get; set; }

        public string PaperDesc { get; set; }

        public double PaperPrice { get; set; }

        public List<PaperQuestionsServiceModel> Questions { get; set; }
    }

    public class PaperQuestionsServiceModel
    {
        public int QueType { get; set; }
        public int DifficultLevel { get; set; }
        public string QueContent { get; set; }
        public string Option0 { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string StandardAnwser { get; set; }
        public string ResolutionTips { get; set; }
        public byte[] ContentImg { get; set; }
        public byte[] Option0Img { get; set; }
        public byte[] Option1Img { get; set; }
        public byte[] Option2Img { get; set; }
        public byte[] Option3Img { get; set; }
        public byte[] Option4Img { get; set; }
        public byte[] Option5Img { get; set; }
    }
}
