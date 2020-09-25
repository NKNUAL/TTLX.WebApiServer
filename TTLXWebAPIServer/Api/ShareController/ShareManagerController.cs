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
    [RoutePrefix("api/share2")]
    [ShareAuth]
    public class ShareManagerController : BaseApiController
    {

        public ShareManagerController(IShareService shareService, IVerifyService verifyService)
            : base(shareService, verifyService) { }

        /// <summary>
        /// 管理员获取试卷
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [Route("paper/get/{page}/{pageSize}")]
        [HttpPost]
        public HttpResultModel GetPapers(int page, int pageSize, PaperQueryModel queryModel)
        {
            var model = Mapper.Map<PaperQueryServiceModel>(queryModel);

            var result = _shareService.GetPaper(page, pageSize, model, null);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 管理员获取试卷
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [Route("users")]
        [HttpGet]
        public HttpResultModel GetBindUsers(string specialtyId)
        {

            var result = _shareService.GetBindUser(specialtyId);

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
        [Route("paper/questions/{paperId}")]
        [HttpGet]
        public HttpResultModel GetQuestions(string paperId)
        {
            var result = _shareService.GetReviewQuestions(paperId);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 获取相似题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/similar_questions/{queNo}")]
        [HttpGet]
        public HttpResultModel GetSimilarQuestions(string queNo)
        {
            var result = _shareService.GetSimilarQuestions(queNo);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        [Route("paper/check/pass")]
        [HttpPost]
        public HttpResultModel CheckPass(CheckPassModel check)
        {
            var reviewResult = _shareService
                .ReviewSharePaper(check.CheckUserId, check.PaperID, (int)CheckStatuDictionary.Pass, null, check.Reason, check.CommentLevel);

            return new HttpResultModel
            {
                success = reviewResult.success,
                message = reviewResult.message,
            };

        }

        /// <summary>
        /// 拒绝
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        [Route("paper/check/refuse")]
        [HttpPost]
        public HttpResultModel CheckRefuse(CheckRefouseModel check)
        {
            var reviewResult = _shareService
                .ReviewSharePaper(check.CheckUserId, check.PaperID, (int)CheckStatuDictionary.Refuse, check.Reason, null, 0);

            return new HttpResultModel
            {
                success = reviewResult.success,
                message = reviewResult.message,
            };
        }

        /// <summary>
        /// 下架试卷
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [Route("paper/putoff/{paperId}")]
        [HttpGet]
        public HttpResultModel PutOff(string paperId)
        {
            var result = _shareService.UpdatePaperStatu(null, paperId, null, (int)PaperStatuDictionary.PutOff);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
            };
        }

    }
}
