using Api.BLL;
using Api.BLL.ServiceModel;
using Api.Core.Enum;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Api.ShareController
{
    [RoutePrefix("api/share")]
    [WebApiExceptionFilter]
    [MachineAuth]
    public class ShareController : BaseApiController
    {

        public ShareController(IShareService shareService, IVerifyService verifyService)
            : base(shareService, verifyService) { }


        /// <summary>
        /// 判断账号使用状态
        /// </summary>
        /// <returns></returns>
        [Route("check/{userToken}")]
        [HttpGet]
        public HttpResultModel CheckStatu(string userToken)
        {
            var bindType = _shareService.CheckUserStatu(userToken);

            HttpResultModel result = new HttpResultModel
            {
                success = true,
                data = (int)bindType
            };

            switch (bindType)
            {
                case BindType.Baned:
                    result.message = "账号被禁用";
                    break;
                case BindType.NotBind:
                    result.message = "未绑定";
                    break;
                case BindType.Pass:
                    result.message = "您已经绑定了";
                    break;
                case BindType.Nono:
                    result.message = "检查错误";
                    break;
            }

            return result;
        }

        /// <summary>
        /// 教师信息绑定
        /// </summary>
        /// <returns></returns>
        [Route("bind")]
        [HttpPost]
        public HttpResultModel UserBind(TeacherBindModel bindModel)
        {

            var model = Mapper.Map<TeacherBindServiceModel>(bindModel);

            var result = _shareService.UserBind(model);

            return new HttpResultModel { success = result.success, message = result.message };
        }

        /// <summary>
        /// 教师出题
        /// </summary>
        /// <param name="uploadModel"></param>
        /// <returns></returns>
        [Route("paper/upload")]
        [HttpPost]
        public HttpResultModel PaperUpload(PaperUploadModel uploadModel)
        {
            var model = Mapper.Map<PaperUploadServiceModel>(uploadModel);

            var result = _shareService.PaperUpload(model);

            return new HttpResultModel { success = result.success, message = result.message };
        }

        /// <summary>
        /// 获取试卷
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="requestUserToken"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [Route("paper/get/{requestUserToken}/{page}/{pageSize}")]
        [HttpPost]
        public HttpResultModel GetPapers(string requestUserToken, int page, int pageSize, PaperQueryModel queryModel)
        {
            var model = Mapper.Map<PaperQueryServiceModel>(queryModel);

            var result = _shareService.GetPaper(page, pageSize, model, requestUserToken);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 获取有共享题库的学校
        /// </summary>
        /// <returns></returns>
        [Route("paper/schools/{specialtyId}")]
        [HttpGet]
        public HttpResultModel GetSchoolForPaper(string specialtyId)
        {
            var result = _shareService.GetSchoolsForPaper(specialtyId);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 试卷上架
        /// </summary>
        /// <param name="requestUserToken"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/puton")]
        [HttpPost]
        public HttpResultModel PaperPutOn(PaperStatuOperModel data)
        {
            var result = _shareService.UpdatePaperStatu(data.RequestUserToken, data.PaperID, null, 1);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
            };
        }

        /// <summary>
        /// 试卷下架
        /// </summary>
        /// <param name="requestUserToken"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/putoff")]
        [HttpPost]
        public HttpResultModel PaperPutOff(PaperStatuOperModel data)
        {
            var result = _shareService.UpdatePaperStatu(data.RequestUserToken, data.PaperID, null, 0);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
            };
        }

        /// <summary>
        /// 购买试卷
        /// </summary>
        /// <param name="requestUserToken"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/buy/{requestUserToken}/{paperId}")]
        [HttpGet]
        public HttpResultModel BuyPaper(string requestUserToken, string paperId)
        {
            var result = _shareService.BuyPaper(requestUserToken, paperId);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };

        }

        /// <summary>
        /// 检查试卷购买状态
        /// </summary>
        /// <param name="requestUserToken"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/buy/check")]
        [HttpGet]
        public HttpResultModel CheckBuyStatu(string orderNo)
        {
            var result = _shareService.CheckBuyStatu(orderNo);

            return new HttpResultModel
            {
                success = true,
                data = result
            };

        }

        /// <summary>
        /// 检查订单状态，关闭订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [Route("paper/order/close/{orderNo}")]
        [HttpGet]
        public HttpResultModel CloseOrder(string orderNo)
        {
            var result = _shareService.CheckAndClose(orderNo);
            return new HttpResultModel
            {
                success = result,
            };
        }

        /// <summary>
        /// 提交评价
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("paper/comment")]
        [HttpPost]
        public HttpResultModel SubmitComment(CommentModel model)
        {
            var comment = Mapper.Map<CommentServiceModel>(model);

            var result = _shareService.Comment(comment);

            return new HttpResultModel { success = result.success, message = result.message };
        }

        /// <summary>
        /// 获取试卷的题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/questions/{paperId}")]
        [HttpGet]
        public HttpResultModel GetPaperQuestions(string paperId)
        {
            var result = _shareService.GetQuestions(paperId);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 获取试卷版本
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/version/{paperId}")]
        [HttpGet]
        public HttpResultModel GetPaperVersion(string paperId)
        {
            var result = _shareService.GetPaperVersion(paperId);
            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 获取试卷题目版本
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/que_version/{paperId}")]
        [HttpGet]
        public HttpResultModel GetQuestionsVersion(string paperId)
        {
            var result = _shareService.GetQuestionVersion(paperId);
            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 获取试卷题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/questionsbyno")]
        [HttpPost]
        public HttpResultModel GetQuestionsByNo(List<string> queNos)
        {
            var result = _shareService.GetQuestions(queNos);
            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }


        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("paper/modify/{paperId}")]
        [HttpPost]
        public HttpResultModel ModifyQuestion(string paperId, QuestionsModel model)
        {
            var ques = Mapper.Map<QuestionsServiceModel>(model);

            var result = _shareService.ModifyQuestions(paperId, ques);

            return new HttpResultModel { success = result.success, message = result.message };
        }

        /// <summary>
        /// 获取试卷题目版本
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/refuse_reason/{paperId}")]
        [HttpGet]
        public HttpResultModel GetRefuseReason(string paperId)
        {
            var result = _shareService.GetRefuseReason(paperId);
            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }


        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("paper/comments/{page}/{pageSize}")]
        [HttpPost]
        public HttpResultModel GetComments(int page, int pageSize, CommentQueryModel query)
        {
            var model = Mapper.Map<CommentQueryServiceModel>(query);

            var result = _shareService.GetComments(page, pageSize, model);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }
    }
}
