using Api.Core.Extensions;
using Api.Core.Logger;
using Api.Core.Redis;
using Api.DAL.DataContext;
using Api.DAL.Entity_MockTestPaper_School;
using Api.Queue.QueueModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.Impl
{
    public class MockTestPaperQueueData : IQueueData
    {
        private ConcurrentQueue<MockTestPaperQueueModel> _queue = new ConcurrentQueue<MockTestPaperQueueModel>();

        private bool _isEnd { get; set; } = true;

        public void Enqueue(object obj)
        {
            if (obj is MockTestPaperQueueModel)
            {
                MockTestPaperQueueModel t = obj as MockTestPaperQueueModel;
                _queue.Enqueue(t);
            }
        }

        Random _random = new Random();

        public async void ToDb()
        {
            if (!_isEnd)
                return;
            _isEnd = false;
            if (_queue.Count > 0)
                LogWriter.Instance.AddLog("模式试卷队列中当前剩余数量：" + _queue.Count);

            bool hasData = _queue.TryDequeue(out var data);
            if (hasData)
            {
                using (DbMockTestPaperSchoolContext db = new DbMockTestPaperSchoolContext())
                {
                    while (hasData)
                    {
                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                var rule = db.UserQuestionRules.FirstOrDefault(u => u.RuleNo == data.RuleNo);
                                #region 题型和分数
                                int danxuanNum =
                                    data.Courses.Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == 1)));
                                int duoxuanNum =
                                    data.Courses.Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == 2)));
                                int pandaunNum =
                                    data.Courses.Sum(c => c.Knows.Sum(k => k.Questions.Count(q => q.QueType == 3)));

                                var types = db.QuestionsType
                                    .Where(q => q.TypeSpecialty.ToString() == rule.SpecialtyId
                                    && new int[] { 1, 2, 3 }.Contains(q.TypeID)).ToList();

                                double danxuanScore = 0;
                                double duoxuanScore = 0;
                                double panduanScore = 0;
                                foreach (var queType in types)
                                {
                                    if (queType.TypeID == 1)
                                        danxuanScore = (danxuanNum * queType.Score) ?? 0;
                                    else if (queType.TypeID == 2)
                                        duoxuanScore = (duoxuanNum * queType.Score) ?? 0;
                                    else
                                        panduanScore = (pandaunNum * queType.Score) ?? 0;
                                }


                                #endregion

                                var mock_paper = new UserQuestionMockTestPaper
                                {
                                    bianchengScore = 0,
                                    excelScore = 0,
                                    pptScore = 0,
                                    wangluoScore = 0,
                                    win7Score = 0,
                                    accessnum = 0,
                                    accessscore = 0,
                                    wordScore = 0,
                                    bianchengNum = 0,
                                    excelNum = 0,
                                    pptNum = 0,
                                    wangluoNum = 0,
                                    wordNum = 0,
                                    win7Num = 0,
                                    isDelete = 0,
                                    PaperType = 5,
                                    RuleNo = data.RuleNo,
                                    FK_Specialty = int.Parse(rule.SpecialtyId),
                                    ExamPaperCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                                    ExamPaperName = data.PaperName,
                                    CreateUserID = data.UserId,
                                    CreateUserName = data.UserName,
                                    danxuanNum = danxuanNum,
                                    duoxuanNum = duoxuanNum,
                                    panduanNum = pandaunNum,
                                    danxuanScore = danxuanScore,
                                    duoxuanScore = duoxuanScore,
                                    panduanScore = panduanScore,
                                    PaperQueCount = danxuanNum + duoxuanNum + pandaunNum,
                                    PaperQueTotalScore = danxuanScore + duoxuanScore + panduanScore,
                                };

                                db.UserQuestionMockTestPaper.Add(mock_paper);
                                await db.SaveChangesAsync();

                                Dictionary<int, int> orderIndex = new Dictionary<int, int>
                                {
                                    { 1, 1 },
                                    { 2, 1 },
                                    { 3, 1 }
                                };

                                foreach (var course in data.Courses)
                                {
                                    if (course.Knows == null)
                                        continue;

                                    foreach (var know in course.Knows)
                                    {
                                        if (know.Questions == null)
                                            continue;

                                        foreach (var que in know.Questions)
                                        {
                                            var queNo = DateTime.Now.ToNormalStringWithout() + _random.Next(1000, 9999) + _random.Next(10, 100);
                                            if (rule.SpecialtyId == "0")
                                            {
                                                db.Questionsinfo_New_Computer.Add(new Questionsinfo_New_Computer
                                                {
                                                    sourcedoc = "UserQuestionMockPaper",
                                                    StandardAnwser = que.Answer,
                                                    FK_SpecialtyType = rule.SpecialtyId,
                                                    CreateTime = mock_paper.ExamPaperCreateTime,
                                                    CreateUserName = data.UserName,
                                                    DifficultLevel = que.DifficultLevel,
                                                    FK_CourseType = course.CourseNo,
                                                    FK_KnowledgePoint = know.KnowNo,
                                                    IsDelete = 0,
                                                    Name = que.QueContent,
                                                    nameImg = (que.NameImg == null || que.NameImg.Length == 0 ? null : que.NameImg),
                                                    Option0 = que.Option0,
                                                    option0Img = (que.Option0Img == null || que.Option0Img.Length == 0 ? null : que.Option0Img),
                                                    Option1 = que.Option1,
                                                    option1Img = (que.Option1Img == null || que.Option1Img.Length == 0 ? null : que.Option1Img),
                                                    Option2 = que.Option2,
                                                    option2Img = (que.Option2Img == null || que.Option2Img.Length == 0 ? null : que.Option2Img),
                                                    Option3 = que.Option3,
                                                    option3Img = (que.Option3Img == null || que.Option3Img.Length == 0 ? null : que.Option3Img),
                                                    Option4 = que.Option4,
                                                    option4Img = (que.Option4Img == null || que.Option4Img.Length == 0 ? null : que.Option4Img),
                                                    Option5 = que.Option5,
                                                    option5Img = (que.Option5Img == null || que.Option5Img.Length == 0 ? null : que.Option5Img),
                                                    ResolutionTips = que.ResolutionTips,
                                                    No = queNo,
                                                    Type = que.QueType,
                                                    UseCount = 0,
                                                    VersionFlag = DateTime.Now.ToNormalStringWithout_ymd(),
                                                });
                                            }
                                            else
                                            {
                                                db.Questionsinfo_New.Add(new Questionsinfo_New
                                                {
                                                    sourcedoc = "UserQuestionMockPaper",
                                                    StandardAnwser = que.Answer,
                                                    FK_SpecialtyType = rule.SpecialtyId,
                                                    CreateTime = mock_paper.ExamPaperCreateTime,
                                                    CreateUserName = data.UserName,
                                                    DifficultLevel = 1,
                                                    FK_CourseType = course.CourseNo,
                                                    FK_KnowledgePoint = know.KnowNo,
                                                    IsDelete = 0,
                                                    Name = que.QueContent,
                                                    nameImg = (que.NameImg == null || que.NameImg.Length == 0 ? null : que.NameImg),
                                                    Option0 = que.Option0,
                                                    option0Img = (que.Option0Img == null || que.Option0Img.Length == 0 ? null : que.Option0Img),
                                                    Option1 = que.Option1,
                                                    option1Img = (que.Option1Img == null || que.Option1Img.Length == 0 ? null : que.Option1Img),
                                                    Option2 = que.Option2,
                                                    option2Img = (que.Option2Img == null || que.Option2Img.Length == 0 ? null : que.Option2Img),
                                                    Option3 = que.Option3,
                                                    option3Img = (que.Option3Img == null || que.Option3Img.Length == 0 ? null : que.Option3Img),
                                                    Option4 = que.Option4,
                                                    option4Img = (que.Option4Img == null || que.Option4Img.Length == 0 ? null : que.Option4Img),
                                                    Option5 = que.Option5,
                                                    option5Img = (que.Option5Img == null || que.Option5Img.Length == 0 ? null : que.Option5Img),
                                                    ResolutionTips = que.ResolutionTips,
                                                    No = queNo,
                                                    Type = que.QueType,
                                                    UseCount = 0,
                                                    VersionFlag = DateTime.Now.ToNormalStringWithout_ymd()
                                                });
                                            }
                                                

                                            db.UserQuestionMockTestPaperQuestionRelation
                                                .Add(new UserQuestionMockTestPaperQuestionRelation
                                                {
                                                    OrderIndex = orderIndex[que.QueType]++,
                                                    PaperID = mock_paper.ExamPaperID.ToString(),
                                                    QuestionID = queNo,
                                                    QuestionType = que.QueType
                                                });

                                            await db.SaveChangesAsync();
                                        }

                                    }

                                }

                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                LogContent.Instance.WriteLog(new AppOpLog()
                                {
                                    LogMessage = "[模拟试卷上传出错]：" + ex.Message + "[内部异常]：" + ex.InnerException?.Message,
                                    MemberID = data.UserId + $"[{data.UserName}]",
                                    MethodName = "[]"
                                }, Log4NetLevel.Error);
                                LogWriter.Instance.AddLog(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                            }
                            hasData = _queue.TryDequeue(out data);
                        }
                    }
                }
            }
            _isEnd = true;
        }
    }
}
