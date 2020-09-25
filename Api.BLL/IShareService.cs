using Api.BLL.ServiceModel;
using Api.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL
{
    public interface IShareService : IDependency
    {
        /// <summary>
        /// 判断用户是否绑定信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        BindType CheckUserStatu(string token);

        /// <summary>
        /// 身份绑定
        /// </summary>
        /// <returns></returns>
        ServiceResult UserBind(TeacherBindServiceModel bindModel);

        /// <summary>
        /// 修改绑定信息
        /// </summary>
        /// <param name="editModel"></param>
        /// <returns></returns>
        ServiceResult EditBindMessage(EditMsgServiceModel editModel);

        /// <summary>
        /// 老师出题上传
        /// </summary>
        /// <param name="paperModel"></param>
        /// <returns></returns>
        ServiceResult PaperUpload(PaperUploadServiceModel paperModel);

        /// <summary>
        /// 获取试卷
        /// </summary>
        /// <returns></returns>
        ServiceResult GetPaper(int page, int pageSize, PaperQueryServiceModel queryModel, string requestUserToken);

        /// <summary>
        /// 获取有共享题库的学校
        /// </summary>
        /// <returns></returns>
        ServiceResult GetSchoolsForPaper(string specialtyId);

        /// <summary>
        /// 修改试卷状态
        /// </summary>
        /// <param name="userToken"></param>
        /// <param name="paperId"></param>
        /// <param name="checkStatu"></param>
        /// <param name="paperStatu"></param>
        /// <returns></returns>
        ServiceResult UpdatePaperStatu(string userToken, string paperId, int? checkStatu, int? paperStatu);

        /// <summary>
        /// 获取试卷题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        ServiceResult GetQuestions(string paperId);

        /// <summary>
        /// 查询订单状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        AliPayStatu QueryOrderStatu(string orderNo);

        /// <summary>
        /// 购买试卷
        /// </summary>
        /// <param name="userToken"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        ServiceResult BuyPaper(string userToken, string paperId);

        /// <summary>
        /// 检查试卷购买状态，如果已经购买，则改变数据库标记
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        bool CheckBuyStatu(string orderNo);

        /// <summary>
        /// 评价试卷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Comment(CommentServiceModel model);

        /// <summary>
        /// 评价试卷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Comment(string paperId, string commnetDesc, double commentLevel, string userId);

        /// <summary>
        /// 检查订单状态，如果订单不是已经付款，或者状态为预创建则直接关闭
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        bool CheckAndClose(string orderNo);

        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="questions"></param>
        /// <returns></returns>
        ServiceResult ModifyQuestions(string paperId, QuestionsServiceModel question);

        /// <summary>
        /// 获取试卷版本
        /// </summary>
        /// <returns></returns>
        ServiceResult GetPaperVersion(string paperId);

        /// <summary>
        /// 获取试卷的题目版本
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        ServiceResult GetQuestionVersion(string paperId);

        /// <summary>
        /// 获取试卷题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="questions"></param>
        /// <returns></returns>
        ServiceResult GetQuestions(List<string> queNos);

        /// <summary>
        /// 获取绑定了的老师
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        ServiceResult GetBindUser(string specialtyId = null);

        /// <summary>
        /// 获取待审核的题目详情
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        ServiceResult GetReviewQuestions(string paperId);

        /// <summary>
        /// 获取相似题目
        /// </summary>
        /// <param name="queNo"></param>
        /// <returns></returns>
        ServiceResult GetSimilarQuestions(string queNo, double Similarity = 0.3);

        /// <summary>
        /// 审核试卷
        /// </summary>
        /// <returns></returns>
        ServiceResult ReviewSharePaper(string userId, string paperId, int checkStatu, string checkReason, string commentDesc, int commentLevel);

        /// <summary>
        /// 获取试卷拒绝原因
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        ServiceResult GetRefuseReason(string paperId);

        /// <summary>
        /// 获取试卷的评价
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        ServiceResult GetComments(int page, int pageSize, CommentQueryServiceModel queryArgs);
    }
}
