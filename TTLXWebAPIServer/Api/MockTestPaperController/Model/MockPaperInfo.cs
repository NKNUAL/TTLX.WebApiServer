using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class MockPaperInfo
    {
        public int PaperId { get; set; }
        public string PaperName { get; set; }
        public string PaperCreateDate { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string RuleNo { get; set; }
        public string RuleName { get; set; }
        public int DanxuanNum { get; set; }
        public int DuoxuanNum { get; set; }
        public int PanduanNum { get; set; }
    }


    public class MockPaperCourseTreeModel
    {
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public int QueCount { get; set; }
        public List<MockPaperKnowTreeModel> Knows { get; set; }
    }

    public class MockPaperKnowTreeModel
    {
        public string KnowNo { get; set; }
        public string KnowName { get; set; }
        public int QueCount { get; set; }
        public List<QuestionsInfoModel> Questions { get; set; }
    }


    public class QuestionsInfoModel
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
    }
}