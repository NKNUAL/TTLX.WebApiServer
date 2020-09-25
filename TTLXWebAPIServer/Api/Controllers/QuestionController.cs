using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.Queue;
using Api.Queue.QueueModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TTLXWebAPIServer.Api.Model;


namespace TTLXWebAPIServer.Controllers
{
    [WebApiExceptionFilter]
    public class QuestionController : ApiController
    {
        /// <summary>
        /// 学校本地题库上传
        /// </summary>
        /// <param name="localQuesions"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<HttpResultModel> Upload(LocalQuestionsModel localQuesions)
        {
            HttpResultModel ret = new HttpResultModel() { Ret_status = "fail" };
            if (localQuesions == null)
                return Json(ret);
            if (localQuesions.SchoolInfo == null)
                return Json(ret);
            if (localQuesions.LocalQuestions == null)
                return Json(ret);
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                try
                {
                    foreach (var item in localQuesions.LocalQuestions)
                    {
                        item.SchoolCode = localQuesions.SchoolInfo.SchoolCode;
                        db.Questionsinfo_Local.Add(item);
                    }
                    db.SaveChanges();
                    ret.Ret_status = "success";
                }
                catch (Exception ex)
                {
                    ret.Ret_status = "fail";
                    ret.Ret_message = ex.Message + Environment.NewLine + ex.InnerException.Message;
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = localQuesions.SchoolInfo.SchoolName,
                        LogMessage = ex.Message,
                        MethodName = "[QuestionController.Upload()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(ret);
        }

        /// <summary>
        /// 学校错题反馈题目上传
        /// </summary>
        /// <param name="errorQuestions"></param>
        /// <returns></returns>
        public JsonResult<HttpResultModel> UploadErrorQuestion(List<ErrorQuestionViewModel> errorQuestions)
        {
            HttpResultModel ret = new HttpResultModel() { Ret_status = "fail" };
            if (errorQuestions == null)
                return Json(ret);

            foreach (var item in errorQuestions)
            {
                var model = Mapper.Map<ErrorQuestionQueueModel>(item);
                GlabolDataExe.Instance.AddData(QueueDataType.ErrorQuestions, model);
            }
            ret.Ret_status = "success";
            return Json(ret);
        }
    }
}
