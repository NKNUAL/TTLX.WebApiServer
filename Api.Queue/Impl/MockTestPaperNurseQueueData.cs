using Api.Core.Enum;
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
    public class MockTestPaperNurseQueueData : IQueueData
    {
        private ConcurrentQueue<MockTestPaperNurseQueueModel> _queue = new ConcurrentQueue<MockTestPaperNurseQueueModel>();

        private bool _isEnd { get; set; } = true;

        public void Enqueue(object obj)
        {
            if (obj is MockTestPaperNurseQueueModel)
            {
                MockTestPaperNurseQueueModel t = obj as MockTestPaperNurseQueueModel;
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
                LogWriter.Instance.AddLog("模式试卷护理队列中当前剩余数量：" + _queue.Count);

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
                                    FK_Specialty = (int)SpecialtyType.SU,
                                    ExamPaperCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                                    ExamPaperName = data.PaperName,
                                    CreateUserID = data.UserId,
                                    CreateUserName = data.UserName,
                                    danxuanNum = 60,//35+10+5(15)
                                    duoxuanNum = 0,
                                    panduanNum = 0,
                                    danxuanScore = 0,
                                    duoxuanScore = 0,
                                    panduanScore = 0,
                                    PaperQueCount = 60,
                                    PaperQueTotalScore = 150,//35*3+10*3+15*1
                                };

                                db.UserQuestionMockTestPaper.Add(mock_paper);
                                await db.SaveChangesAsync();

                                int a3Index = 1;
                                foreach (var item in data.A_)
                                {
                                    if (item.TypeId == 3)//A3题型
                                    {
                                        var generalNo = DateTime.Now.ToNormalStringWithout() + _random.Next(1000, 9999) + _random.Next(10, 100);
                                        db.UserQuestionMockTestPaperGeneral.Add(new UserQuestionMockTestPaperGeneral
                                        {
                                            GeneralNo = generalNo,
                                            GeneralQueName = item.GeneralName,
                                            NameImg = item.NameImg,
                                        });
                                        db.UserQuestionMockTestPaperGeneralRelation.Add(new UserQuestionMockTestPaperGeneralRelation
                                        {
                                            OrderIndex = a3Index++,
                                            GeneralNo = generalNo,
                                            PaperID = mock_paper.ExamPaperID.ToString()
                                        });

                                        await db.SaveChangesAsync();

                                        int queIndex = 1;
                                        foreach (var que in item.Questions)
                                        {
                                            var queNo = DateTime.Now.ToNormalStringWithout() + _random.Next(1000, 9999) + _random.Next(10, 100);

                                            db.UserQuestionGeneralQuestionRelation.Add(new UserQuestionGeneralQuestionRelation
                                            {
                                                GeneralNo = generalNo,
                                                OrderIndex = queIndex++,
                                                QueNo = queNo
                                            });

                                            db.Questionsinfo_New.Add(new Questionsinfo_New
                                            {
                                                sourcedoc = "UserQuestionMockPaper",
                                                StandardAnwser = que.Answer,
                                                FK_SpecialtyType = ((int)SpecialtyType.SU).ToString(),
                                                CreateTime = mock_paper.ExamPaperCreateTime,
                                                CreateUserName = data.UserName,
                                                DifficultLevel = que.DifficultLevel,
                                                FK_CourseType = que.CourseNo,
                                                FK_KnowledgePoint = que.KnowNo,
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

                                        await db.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        int queIndex = 1;
                                        foreach (var que in item.Questions)
                                        {
                                            var queNo = DateTime.Now.ToNormalStringWithout() + _random.Next(1000, 9999) + _random.Next(10, 100);

                                            db.UserQuestionMockTestPaperQuestionRelation.Add(new UserQuestionMockTestPaperQuestionRelation
                                            {
                                                OrderIndex = queIndex++,
                                                PaperID = mock_paper.ExamPaperID.ToString(),
                                                QuestionID = queNo,
                                                QuestionType = item.TypeId
                                            });

                                            db.Questionsinfo_New.Add(new Questionsinfo_New
                                            {
                                                sourcedoc = "UserQuestionMockPaper",
                                                StandardAnwser = que.Answer,
                                                FK_SpecialtyType = ((int)SpecialtyType.SU).ToString(),
                                                CreateTime = mock_paper.ExamPaperCreateTime,
                                                CreateUserName = data.UserName,
                                                DifficultLevel = que.DifficultLevel,
                                                FK_CourseType = que.CourseNo,
                                                FK_KnowledgePoint = que.KnowNo,
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

                                        await db.SaveChangesAsync();
                                    }
                                }


                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                LogContent.Instance.WriteLog(new AppOpLog()
                                {
                                    LogMessage = "[护理专业模拟试卷上传出错]：" + ex.Message + "[内部异常]：" + ex.InnerException?.Message,
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
