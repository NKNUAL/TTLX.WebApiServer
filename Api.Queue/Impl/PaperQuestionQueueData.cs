using Api.Core.Enum;
using Api.Core.Extensions;
using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.DAL.Entity_SharePaper;
using Api.Queue.QueueModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.Impl
{
    class PaperQuestionQueueData : IQueueData
    {
        private ConcurrentQueue<PaperUploadQueueModel> _queue = new ConcurrentQueue<PaperUploadQueueModel>();

        public bool _isEnd = true;
        public void Enqueue(object obj)
        {
            if (obj is PaperUploadQueueModel)
            {
                PaperUploadQueueModel t = obj as PaperUploadQueueModel;
                _queue.Enqueue(t);
            }
        }

        public void ToDb()
        {
            if (!_isEnd)
                return;
            _isEnd = false;
                if (_queue.Count > 0)
                LogWriter.Instance.AddLog("共享题库队列中当前剩余数量：" + _queue.Count);

            bool hasData = _queue.TryDequeue(out PaperUploadQueueModel data);

            if (hasData)
            {
                using (DbShareContext db = new DbShareContext())
                {

                    while (hasData)
                    {
                        var tran = db.Database.BeginTransaction();

                        try
                        {

                            Random random = new Random();

                            var bind = db.UserBindInfo.FirstOrDefault(u => u.UserToken == data.UserToken);

                            string dateNow = DateTime.Now.ToNormalString();

                            PaperInfo paperInfo = new PaperInfo
                            {
                                CheckStatu = (int)Core.Enum.CheckStatuDictionary.Pending,
                                PaperSpecialtyId = bind.SpecialtyId,
                                PaperStatu = (int)Core.Enum.PaperStatuDictionary.PutOff,
                                PaperCreateDate = dateNow,
                                PaperDesc = data.PaperDesc,
                                PaperName = data.PaperName,
                                PaperPrice = data.PaperPrice,
                                PaperUserId = bind.UserId,
                                PaperQueCount = data.Questions.Count,
                                DanxuanNum = data.Questions.Count(q => q.QueType == (int)QuestionType.Danxuan),
                                DuoxuanNum = data.Questions.Count(q => q.QueType == (int)QuestionType.Duoxuan),
                                PanduanNum = data.Questions.Count(q => q.QueType == (int)QuestionType.Panduan),
                                PaperID = DateTime.Now.ToNormalStringWithout() + random.Next(1000, 9999),
                                PaperVersion = DateTime.Now.ToNormalStringWithout()
                            };


                            db.PaperInfo.Add(paperInfo);

                            Random r = new Random(data.Questions.Count);

                            foreach (var item in data.Questions)
                            {
                                QuestionsInfo queInfo = new QuestionsInfo
                                {
                                    No = Guid.NewGuid().ToString(),
                                    DifficultLevel = item.DifficultLevel,
                                    SpecialtyId = bind.SpecialtyId,
                                    StandardAnwser = item.StandardAnwser,
                                    QueContent = item.QueContent,
                                    ContentImg = item.ContentImg != null ? (item.ContentImg.Length > 0 ? item.ContentImg : null) : null,
                                    Option0 = item.Option0,
                                    Option0Img = item.Option0Img != null ? (item.Option0Img.Length > 0 ? item.Option0Img : null) : null,
                                    Option1 = item.Option1,
                                    Option1Img = item.Option1Img != null ? (item.Option1Img.Length > 0 ? item.Option1Img : null) : null,
                                    Option2 = item.Option2,
                                    Option2Img = item.Option2Img != null ? (item.Option2Img.Length > 0 ? item.Option2Img : null) : null,
                                    Option3 = item.Option3,
                                    Option3Img = item.Option3Img != null ? (item.Option3Img.Length > 0 ? item.Option3Img : null) : null,
                                    Option4 = item.Option4,
                                    Option4Img = item.Option4Img != null ? (item.Option4Img.Length > 0 ? item.Option4Img : null) : null,
                                    Option5 = item.Option5,
                                    Option5Img = item.Option5Img != null ? (item.Option5Img.Length > 0 ? item.Option5Img : null) : null,
                                    QueType = item.QueType,
                                    ResolutionTips = item.ResolutionTips,
                                    IsDelete = 0,
                                    QueVersion = DateTime.Now.ToNormalStringWithout()
                                };
                                db.QuestionsInfo.Add(queInfo);

                                db.PaperQuestionsRelation.Add(new PaperQuestionsRelation
                                {
                                    PaperID = paperInfo.PaperID,
                                    QueNo = queInfo.No,
                                });
                            }

                            db.SaveChanges();
                            tran.Commit();

                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();

                            LogContent.Instance.WriteLog(new AppOpLog()
                            {
                                LogMessage = "[共享题库上传出错]：" + ex.Message + "[内部异常]：" + ex.InnerException?.Message,
                                MemberID = "学生数据上传",
                                MethodName = "[]"
                            }, Log4NetLevel.Error);
                            _queue.Enqueue(data);
                        }
                        finally
                        {
                            tran.Dispose();
                        }
                        hasData = _queue.TryDequeue(out data);
                    }
                }
            }

            _isEnd = true;

        }
    }
}
