using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.QueueModel
{
    public class MockTestPaperQueueModel
    {
        public string RuleNo { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PaperName { get; set; }
        public List<PutQuestionCourseQueueModel> Courses { get; set; }
    }

    public class PutQuestionCourseQueueModel
    {
        public string CourseNo { get; set; }
        public List<PutQuestionKnowQueueModel> Knows { get; set; }
    }

    public class PutQuestionKnowQueueModel
    {
        public string KnowNo { get; set; }
        public List<QuestionsInfoQueueModel> Questions { get; set; }
    }

    public class QuestionsInfoQueueModel
    {
        public string No { get; set; }
        public string QueContent { get; set; }
        public int QueType { get; set; }
        public string Option0 { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string ResolutionTips { get; set; }
        public string Answer { get; set; }
        public byte[] NameImg { get; set; }
        public byte[] Option0Img { get; set; }
        public byte[] Option1Img { get; set; }
        public byte[] Option2Img { get; set; }
        public byte[] Option3Img { get; set; }
        public byte[] Option4Img { get; set; }
        public byte[] Option5Img { get; set; }
        public int DifficultLevel { get; set; }
    }
}
