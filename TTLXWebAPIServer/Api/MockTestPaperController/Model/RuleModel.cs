using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class RuleModel
    {
        public string RuleNo { get; set; }
        public string RuleName { get; set; }
        public string RuleDesc { get; set; }
        public string SpecialtyId { get; set; }
        public string CourseNo { get; set; }
        public int CoureseQueCount { get; set; }
        public string KnowNo { get; set; }
        public int KnowQueCount { get; set; }
    }
}