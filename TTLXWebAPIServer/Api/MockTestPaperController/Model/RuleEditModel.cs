using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class RuleEditModel
    {
        public string RuleNo { get; set; }
        public int QueCount { get; set; }
        public List<RuleEditCourseModel> Courses { get; set; }
    }

    public class RuleEditCourseModel
    {
        public string CourseNo { get; set; }
        public int QueCount { get; set; }
        public List<RuleEditKnowModel> Knows { get; set; }
    }

    public class RuleEditKnowModel
    {
        public string KnowNo { get; set; }
        /// <summary>
        /// 0--修改；1--增加；2--删除
        /// </summary>
        public int EditType { get; set; }
        public int QueCount { get; set; }

    }
}