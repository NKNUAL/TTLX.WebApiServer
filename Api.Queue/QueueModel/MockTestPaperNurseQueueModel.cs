﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.QueueModel
{
    /// <summary>
    /// 护理专业题目
    /// </summary>
    public class MockTestPaperNurseQueueModel
    {
        public string RuleNo { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PaperName { get; set; }
        public List<PutQuestionA_QueueModel> A_ { get; set; }
    }
    public class PutQuestionA_QueueModel
    {
        public string GeneralNo { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string GeneralName { get; set; }
        public byte[] NameImg { get; set; }
        public List<QuestionsInfoQueueModel2> Questions { get; set; }
    }

    public class QuestionsInfoQueueModel2
    {
        public string No { get; set; }
        public string CourseNo { get; set; }
        public string KnowNo { get; set; }
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
