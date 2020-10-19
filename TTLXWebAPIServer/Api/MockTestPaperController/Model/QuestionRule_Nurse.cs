using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class QuestionRule_Nurse
    {
        public string RuleNo { get; set; }
        public string RuleName { get; set; }
        public string RuleDesc { get; set; }
        public string SpecialtyId { get; set; }
        public List<NurseQuestionRule> A_ { get; set; }
    }


    public class NurseQuestionRule
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int QueCount { get; set; }
        public int SubQueCount { get; set; }
    }

}