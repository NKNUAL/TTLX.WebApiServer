using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class QuestionRule
    {
        public string RuleNo { get; set; }
        public string RuleName { get; set; }
        public string RuleDesc { get; set; }
        public string SpecialtyId { get; set; }
        public List<SubRule> CourseRules { get; set; }
    }

    public class SubRule
    {
        public string No { get; set; }
        public string Name { get; set; }
        public int QueCount { get; set; }
        public List<SubRule> KnowRules { get; set; }
    }
}