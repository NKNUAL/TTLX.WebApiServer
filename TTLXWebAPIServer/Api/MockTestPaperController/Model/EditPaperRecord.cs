using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class EditPaperRecord
    {
        public string RuleNo { get; set; }
        public string PGuid { get; set; }
        public string EditDate { get; set; }
        public Dictionary<string, Dictionary<string, List<QuestionsInfoModel>>> DicQuestions { get; set; }
    }
}