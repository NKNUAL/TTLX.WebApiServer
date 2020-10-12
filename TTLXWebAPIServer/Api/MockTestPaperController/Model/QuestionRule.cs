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
        public List<CourseRule> CourseRules { get; set; }
    }

    public class CourseRule
    {
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public int QueCount { get; set; }
        public int DanxuanCount { get; set; }
        public int DuoxuanCount { get; set; }
        public int PanduanCount { get; set; }
        public List<KnowRule> KnowRules { get; set; }
    }

    public class KnowRule
    {
        public string KnowNo { get; set; }
        public string KnowName { get; set; }
        public int? DanxuanCount { get; set; }
        public int? DuoxuanCount { get; set; }
        public int? PanduanCount { get; set; }
    }
}