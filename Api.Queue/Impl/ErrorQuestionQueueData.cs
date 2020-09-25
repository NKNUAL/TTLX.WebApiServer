using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.DAL.Entity_Users;
using Api.Queue.QueueModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.Impl
{
    public class ErrorQuestionQueueData : IQueueData
    {
        private ConcurrentQueue<ErrorQuestionQueueModel> _queue = new ConcurrentQueue<ErrorQuestionQueueModel>();

        public bool _isEnd = true;

        public void Enqueue(object obj)
        {
            if (obj is ErrorQuestionQueueModel)
            {
                ErrorQuestionQueueModel t = obj as ErrorQuestionQueueModel;
                _queue.Enqueue(t);
            }
        }

        public void ToDb()
        {

            if (!_isEnd)
                return;

            _isEnd = false;

            if (_queue.Count > 0)
                LogWriter.Instance.AddLog("队列中当前剩余错题数量：" + _queue.Count);

            bool hasData = _queue.TryDequeue(out ErrorQuestionQueueModel data);

            if (hasData)
            {
                using (DbUsersContext db = new DbUsersContext())
                {
                    while (hasData)
                    {

                        try
                        {
                            db.ErrorQuestionsFeedbackRecord.Add(new ErrorQuestionsFeedbackRecord
                            {
                                SchoolCode = data.SchoolCode,
                                SubmitDate = data.SubmitDate,
                                SubmitUserId = data.SubmitUserId,
                                ErrorDesc = data.ErrorDesc,
                                ErrorTag = data.ErrorTag,
                                QuestionId = data.QuestionId
                            });
                            var que = db.ErrorQuestions.FirstOrDefault(e => e.QuestionId == data.QuestionId);
                            if (que == null)
                            {
                                var quebase = db.V_TotalQuestion.FirstOrDefault(t => t.QueNo == data.QuestionId);
                                int? dbType = quebase?.DbType;

                                db.ErrorQuestions.Add(new ErrorQuestions
                                {
                                    SpecialtyCode = data.SpecialtyCode,
                                    DbType = dbType ?? 1,
                                    IsCorrect = 0,
                                    QuestionId = data.QuestionId,
                                    QuestionType = data.QuestionType,
                                });
                            }
                            else
                            {
                                que.IsCorrect = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogWriter.Instance.AddLog("增加错题出错：" + ex.Message);

                            LogWriter.Instance.AddLog(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        }

                        hasData = _queue.TryDequeue(out data);
                    }
                    db.SaveChanges();
                }
            }

            _isEnd = true;

        }
    }
}
