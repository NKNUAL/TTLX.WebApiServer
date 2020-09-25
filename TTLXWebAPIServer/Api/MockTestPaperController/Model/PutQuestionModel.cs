using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class PutQuestionModel
    {
        public string RuleNo { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PaperName { get; set; }
        public List<PutQuestionCourseModel> Courses { get; set; }
    }

    public class PutQuestionCourseModel
    {
        public string CourseNo { get; set; }
        public List<PutQuestionKnowModel> Knows { get; set; }
    }

    public class PutQuestionKnowModel
    {
        public string KnowNo { get; set; }
        public List<QuestionsInfoModel> Questions { get; set; }
    }
}