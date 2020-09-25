using Api.BLL.Helper;
using Api.BLL.ServiceModel;
using Api.Core;
using Api.Core.AliPay;
using Api.Core.Enum;
using Api.Core.Extensions;
using Api.Core.Logger;
using Api.DAL;
using Api.DAL.DataContext;
using Api.Queue;
using Api.Queue.QueueModel;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.Impl
{
    public class ShareService : IShareService
    {
        private readonly DbShareContext _dbShare = DbContextFactory.GetDbShare();

        public ServiceResult BuyPaper(string userToken, string paperId)
        {

            try
            {
                var bind = _dbShare.UserBindInfo.FirstOrDefault(u => u.UserToken == userToken);

                if (bind != null)
                {
                    if (bind.UseStatu == 0)
                        return new ServiceResult { success = false, message = "对不起，您没有权限购买试卷" };

                    int count = _dbShare.OrderRecord.Count(o => o.PaperID == paperId && o.CreateUserId == bind.UserId && o.OrderStatu == (int)OrderStatuDictionary.Finished);
                    if (count > 0)
                        return new ServiceResult { success = false, message = "您已经购买过此试卷，可直接使用。" };

                    var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);

                    if (paperInfo == null)
                        return new ServiceResult { success = false, message = "试卷不存在！" };

                    if (paperInfo.PaperUserId == bind.UserId)
                        return new ServiceResult { success = false, message = "您无需购买您自己的试卷！" };

                    string orderNo = ChineseToPinyinHelper.ConvertToSP(bind.ZhifubaoName) +
                       DateTime.Now.ToNormalStringWithout() + new Random().Next(1000, 9999);
                    _dbShare.OrderRecord.Add(new DAL.Entity_SharePaper.OrderRecord
                    {
                        OrderStatu = (int)OrderStatuDictionary.PreCreated,
                        CreateUserId = bind.UserId,
                        OrderDate = DateTime.Now.ToNormalString(),
                        ExpireDate = DateTime.Now.AddYears(1).ToNormalString(),
                        OrderNo = orderNo,
                        OrderPrice = paperInfo.PaperPrice,
                        PaperID = paperInfo.PaperID,
                        PayType = 1,
                    });
                    _dbShare.SaveChanges();

                    string qrcode = AliPayHelper.Instance.CreateOrder(orderNo, paperInfo.PaperPrice, "天天乐学试卷", paperInfo.PaperName, paperInfo.PaperID);

                    if (qrcode == null)
                        return new ServiceResult { success = false, message = "订单创建失败，请稍后再试" };

                    OrderHelper.Instance.orderNos.Add(orderNo);

                    return new ServiceResult { success = true, data = new { OrderNo = orderNo, Qrcode = qrcode } };

                }
                return new ServiceResult { success = false, message = "您尚未绑定信息" };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = userToken,
                    LogMessage = "购买试卷:" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "订单创建异常，请稍后再试" };
            }
        }

        public bool CheckAndClose(string orderNo)
        {
            try
            {
                var statu = AliPayHelper.Instance.QueryOrder(orderNo);
                if (statu == AliPayStatu.Paid || statu == AliPayStatu.Closed)
                {
                    var order = _dbShare.OrderRecord.FirstOrDefault(o => o.OrderNo == orderNo);
                    if (order != null)
                    {
                        order.OrderStatu = statu == AliPayStatu.Paid ?
                            (int)OrderStatuDictionary.Finished : (int)OrderStatuDictionary.Closed;
                        _dbShare.SaveChanges();
                    }
                }
                if (statu == AliPayStatu.Nono)
                {
                    var order = _dbShare.OrderRecord.FirstOrDefault(o => o.OrderNo == orderNo);
                    if (order != null)
                    {
                        order.OrderStatu = (int)OrderStatuDictionary.Closed;
                        _dbShare.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = $"查询订单【{orderNo}】:" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return false;
            }
        }

        public bool CheckBuyStatu(string orderNo)
        {
            try
            {
                var statu = AliPayHelper.Instance.QueryOrder(orderNo);
                if (statu == AliPayStatu.Paid || statu == AliPayStatu.Closed)
                {
                    var order = _dbShare.OrderRecord.FirstOrDefault(o => o.OrderNo == orderNo);
                    if (order != null)
                    {
                        order.OrderStatu = statu == AliPayStatu.Paid ?
                            (int)OrderStatuDictionary.Finished : (int)OrderStatuDictionary.Closed;
                    }
                    _dbShare.SaveChanges();
                }
                return statu == AliPayStatu.Paid;
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = $"查询订单【{orderNo}】:" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return false;
            }
        }

        public BindType CheckUserStatu(string token)
        {
            try
            {
                var bindInfo = _dbShare.UserBindInfo.FirstOrDefault(u => u.UserToken == token);

                if (bindInfo == null)
                    return BindType.NotBind;

                if (bindInfo.UseStatu == 0)
                    return BindType.Baned;

                return BindType.Pass;
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = token,
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return BindType.Nono;
            }

        }

        public ServiceResult Comment(CommentServiceModel model)
        {
            try
            {
                var bindInfo = _dbShare.UserBindInfo.FirstOrDefault(u => u.UserToken == model.UserToken);
                if (bindInfo == null)
                    return new ServiceResult { success = false, message = "未绑定，无法评价试卷" };

                var comment = _dbShare.CommentRecord
                    .FirstOrDefault(c => c.CommentUserId == bindInfo.UserId && c.PaperID == model.PaperID && c.IsDelete == 0);

                if (comment == null)
                {
                    _dbShare.CommentRecord.Add(new DAL.Entity_SharePaper.CommentRecord
                    {
                        CommentDate = DateTime.Now.ToNormalString(),
                        CommentDesc = model.CommentDesc,
                        CommentNo = DateTime.Now.ToNormalStringWithout() + new Random().Next(1000, 9999),
                        CommentLevel = model.CommentLevel,
                        CommentUserId = bindInfo.UserId,
                        IsAnonymous = model.IsAnonymous ? 1 : 0,
                        IsDelete = 0,
                        PaperID = model.PaperID
                    });
                }
                else
                {
                    comment.IsAnonymous = model.IsAnonymous ? 1 : 0;
                    comment.CommentLevel = model.CommentLevel;
                    comment.CommentDesc = model.CommentDesc;
                }

                _dbShare.SaveChanges();
                return new ServiceResult { success = true };

            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = model.UserToken,
                    LogMessage = "评价试卷出错：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "服务异常，评价出错" };
            }

        }

        public ServiceResult Comment(string paperId, string commnetDesc, double commentLevel, string userId)
        {
            try
            {
                _dbShare.CommentRecord.Add(new DAL.Entity_SharePaper.CommentRecord
                {
                    CommentDate = DateTime.Now.ToNormalString(),
                    CommentDesc = commnetDesc,
                    CommentNo = DateTime.Now.ToNormalStringWithout() + new Random().Next(1000, 9999),
                    CommentLevel = (int)commentLevel,
                    CommentUserId = userId,
                    IsAnonymous = 0,
                    IsDelete = 0,
                    PaperID = paperId
                });
                _dbShare.SaveChanges();
                return new ServiceResult { success = true };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = userId,
                    LogMessage = "评价试卷出错：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "服务异常，评价出错" };
            }
        }

        public ServiceResult EditBindMessage(EditMsgServiceModel editModel)
        {
            try
            {
                var bindInfo = _dbShare.UserBindInfo.FirstOrDefault(u => u.UserToken == editModel.UserToken);

                if (bindInfo != null)
                {
                    if (bindInfo.UseStatu == 1)
                    {
                        bindInfo.Zhifubao = editModel.Zhifubao;
                        bindInfo.ZhifubaoName = editModel.ZhifubaoName;
                        _dbShare.SaveChanges();
                        return new ServiceResult { success = true };
                    }
                    else
                    {
                        return new ServiceResult { success = false, message = "无法修改信息。" };
                    }
                }
                return new ServiceResult { success = false, message = "未绑定，请先绑定。" };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = editModel.UserToken,
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "绑定发生错误" };
            }
        }

        public ServiceResult GetBindUser(string specialtyId = null)
        {
            try
            {
                var query = _dbShare.UserBindInfo.Where(u => u.UseStatu == 1);

                if (!string.IsNullOrEmpty(specialtyId))
                    query = query.Where(q => q.SpecialtyId == specialtyId);

                var users = (from a in query
                             join b in _dbShare.Base_School on a.SchoolNo equals b.SchoolNo
                             join c in _dbShare.Base_specialtyType on a.SpecialtyId equals c.No
                             select new BindUserModel
                             {
                                 UserToken = a.UserToken,
                                 UserName = a.UserName,
                                 SchoolName = b.SchoolName,
                                 SchoolNo = b.SchoolNo,
                                 SpecialtyId = c.No,
                                 SpecialtyName = c.Name
                             }).ToList();
                return new ServiceResult { success = true, data = users };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取绑定用户失败" };
            }



        }

        public ServiceResult GetComments(int page, int pageSize, CommentQueryServiceModel queryArgs)
        {
            try
            {
                var commentsQuery = _dbShare.CommentRecord.Where(c => c.PaperID == queryArgs.PaperID);

                var query = from a in commentsQuery
                            join b in _dbShare.UserBindInfo on a.CommentUserId equals b.UserId
                            where a.PaperID == queryArgs.PaperID
                            select new CommentDataServiceModel
                            {
                                CommentDate = a.CommentDate,
                                CommentDesc = a.CommentDesc,
                                CommentLevel = a.CommentLevel,
                                UserName = a.IsAnonymous == 1 ? "匿名" : b.UserName
                            };

                switch (queryArgs.OrderType)
                {
                    case (int)PaperOrderType.Score:
                        if (string.IsNullOrEmpty(queryArgs.OrderBy) || queryArgs.OrderBy.Equals("desc"))
                            query = query.OrderByDescending(c => c.CommentLevel);
                        else
                            query = query.OrderBy(q => q.CommentLevel);
                        break;
                    default:
                        if (string.IsNullOrEmpty(queryArgs.OrderBy) || queryArgs.OrderBy.Equals("desc"))
                            query = query.OrderByDescending(c => c.CommentDate);
                        else
                            query = query.OrderBy(c => c.CommentDate);
                        break;
                }

                CommentsServiceModel model = new CommentsServiceModel
                {
                    TotalCount = query.Count(),
                    CommentData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList()
                };

                return new ServiceResult { success = true, data = model };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = "获取评论失败：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "服务异常！" };
            }

        }

        public ServiceResult GetPaper(int page, int pageSize, PaperQueryServiceModel queryModel, string requestUserToken)
        {
            try
            {

                var paperQuery = _dbShare.PaperInfo.AsQueryable();

                if (!string.IsNullOrEmpty(queryModel.SpecialtyId))
                    paperQuery = paperQuery.Where(p => p.PaperSpecialtyId == queryModel.SpecialtyId);

                if (queryModel.PaperStatu != null)
                    paperQuery = paperQuery.Where(q => q.PaperStatu == queryModel.PaperStatu);

                if (queryModel.CheckStatu != null)
                    paperQuery = paperQuery.Where(q => q.CheckStatu == queryModel.CheckStatu);

                var bindQuery = _dbShare.UserBindInfo.AsQueryable();

                if (!string.IsNullOrEmpty(queryModel.SchoolNo))
                    bindQuery = bindQuery.Where(q => q.SchoolNo == queryModel.SchoolNo);

                if (!string.IsNullOrEmpty(queryModel.UseToken))
                    bindQuery = bindQuery.Where(q => q.UserToken == queryModel.UseToken);

                int tempss = paperQuery.Count();

                var shareQuery = from a in paperQuery
                                 join b in bindQuery on a.PaperUserId equals b.UserId
                                 select new
                                 {
                                     a.PaperID,
                                     a.PaperName,
                                     a.PaperDesc,
                                     a.PaperCreateDate,
                                     a.PaperQueCount,
                                     a.PaperPrice,
                                     CreateUserName = b.UserName,
                                     a.CheckStatu,
                                     a.PaperStatu,
                                     a.PaperVersion
                                 };

                tempss = shareQuery.Count();

                if (!string.IsNullOrEmpty(queryModel.BoughtUserToken))
                {
                    var boughtToken = _dbShare.UserBindInfo.FirstOrDefault(u => u.UserToken == queryModel.BoughtUserToken);
                    if (boughtToken != null)
                    {
                        string dateNow = DateTime.Now.ToNormalString();
                        shareQuery = from a in shareQuery
                                     join b in _dbShare.OrderRecord on a.PaperID equals b.PaperID
                                     where b.CreateUserId == boughtToken.UserId
                                     && b.OrderStatu == (int)OrderStatuDictionary.Finished
                                     && b.ExpireDate.CompareTo(dateNow) > 0
                                     select a;
                        tempss = shareQuery.Count();
                    }
                }


                var orderQuery = _dbShare.OrderRecord
                    .Where(o => o.OrderStatu == (int)OrderStatuDictionary.Finished)
                    .GroupBy(o => o.PaperID)
                    .Select(o => new { PaperID = o.Key, OrderCount = o.Count() });
                var scoreQuery = _dbShare.CommentRecord
                    .Where(o => o.IsDelete == 0)
                    .GroupBy(o => o.PaperID)
                    .Select(o => new { PaperID = o.Key, Score = o.Average(q => q.CommentLevel) });

                var query = from a in shareQuery
                            join b in orderQuery on a.PaperID equals b.PaperID into temp_ab
                            from ab in temp_ab.DefaultIfEmpty()
                            join c in scoreQuery on a.PaperID equals c.PaperID into temp_ac
                            from ac in temp_ac.DefaultIfEmpty()
                            select new SharePaperServiceModel
                            {
                                PaperID = a.PaperID,
                                PaperName = a.PaperName,
                                PaperDesc = a.PaperDesc,
                                PaperCreateDate = a.PaperCreateDate,
                                PaperQueCount = a.PaperQueCount,
                                PaperPrice = a.PaperPrice,
                                CreateUserName = a.CreateUserName,
                                CommentLevel = ac.Score,
                                PurchaseNumber = ab.OrderCount,
                                PaperStatu = a.PaperStatu,
                                CheckStatu = a.CheckStatu,
                                PaperVersion = a.PaperVersion
                            };

                tempss = query.Count();

                switch (queryModel.OrderType)
                {
                    case (int)PaperOrderType.NumberOfPurchasers:
                        if (string.IsNullOrEmpty(queryModel.OrderBy) || queryModel.OrderBy.Equals("desc"))
                            query = query.OrderByDescending(q => q.PurchaseNumber);
                        else
                            query = query.OrderBy(q => q.PurchaseNumber);
                        break;

                    case (int)PaperOrderType.Price:
                        if (string.IsNullOrEmpty(queryModel.OrderBy) || queryModel.OrderBy.Equals("desc"))
                            query = query.OrderByDescending(q => q.PaperPrice);
                        else
                            query = query.OrderBy(q => q.PaperPrice);
                        break;

                    case (int)PaperOrderType.Score:
                        if (string.IsNullOrEmpty(queryModel.OrderBy) || queryModel.OrderBy.Equals("desc"))
                            query = query.OrderByDescending(q => q.CommentLevel);
                        else
                            query = query.OrderBy(q => q.CommentLevel);
                        break;
                    default:
                        if (string.IsNullOrEmpty(queryModel.OrderBy) || queryModel.OrderBy.Equals("desc"))
                            query = query.OrderByDescending(q => q.PaperCreateDate);
                        else
                            query = query.OrderBy(q => q.PaperCreateDate);
                        break;
                }


                int totalCount = query.Count();

                var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                if (!string.IsNullOrEmpty(requestUserToken))
                {
                    var orderBuyPaperIDs = (from a in _dbShare.OrderRecord
                                            join b in _dbShare.UserBindInfo on a.CreateUserId equals b.UserId
                                            where a.OrderStatu == (int)OrderStatuDictionary.Finished
                                            && b.UserToken == requestUserToken
                                            select a.PaperID).ToList();

                    data.ForEach(d =>
                    {
                        if (orderBuyPaperIDs.Contains(d.PaperID))
                        {
                            d.IsBought = true;
                        }
                    });
                }


                return new ServiceResult
                {
                    success = true,
                    data = new SharePaperTotalServiceModel
                    {
                        TotalCount = totalCount,
                        PaperData = data
                    }
                };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = requestUserToken ?? "admin",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "服务器资源异常，请稍后再试！" };
            }

        }

        public ServiceResult GetPaperVersion(string paperId)
        {
            try
            {
                var paper = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);
                if (paper == null)
                    return new ServiceResult { success = false, message = "试卷不存在" };
                return new ServiceResult { success = true, data = paper.PaperVersion };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = "获取试卷版本：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);
                return new ServiceResult { success = false, message = "获取试卷版本失败" };
            }
        }

        public ServiceResult GetQuestions(string paperId)
        {
            try
            {
                var ques = (from a in _dbShare.PaperQuestionsRelation
                            join b in _dbShare.QuestionsInfo on a.QueNo equals b.No
                            where a.PaperID == paperId
                            select new QuestionsServiceModel
                            {
                                QueNo = b.No,
                                QueType = b.QueType ?? 1,
                                QueContent = b.QueContent,
                                ContentImg = b.ContentImg,
                                Option0 = b.Option0,
                                Option0Img = b.Option0Img,
                                Option1 = b.Option1,
                                Option1Img = b.Option1Img,
                                Option2 = b.Option2,
                                Option2Img = b.Option2Img,
                                Option3 = b.Option3,
                                Option3Img = b.Option3Img,
                                Option4 = b.Option4,
                                Option4Img = b.Option4Img,
                                Option5 = b.Option5,
                                Option5Img = b.Option5Img,
                                StandardAnwser = b.StandardAnwser,
                                DifficultLevel = b.DifficultLevel ?? 1,
                                ResolutionTips = b.ResolutionTips,
                                QueVersion = b.QueVersion
                            }).ToList();

                return new ServiceResult { success = true, data = ques };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取题目失败，服务器资源异常，请稍后再试！" };
            }
        }

        public ServiceResult GetQuestions(List<string> queNos)
        {
            try
            {
                var ques = _dbShare.QuestionsInfo
                    .Where(q => queNos.Contains(q.No))
                    .Select(b => new QuestionsServiceModel
                    {
                        QueNo = b.No,
                        QueType = b.QueType ?? 1,
                        QueContent = b.QueContent,
                        ContentImg = b.ContentImg,
                        Option0 = b.Option0,
                        Option0Img = b.Option0Img,
                        Option1 = b.Option1,
                        Option1Img = b.Option1Img,
                        Option2 = b.Option2,
                        Option2Img = b.Option2Img,
                        Option3 = b.Option3,
                        Option3Img = b.Option3Img,
                        Option4 = b.Option4,
                        Option4Img = b.Option4Img,
                        Option5 = b.Option5,
                        Option5Img = b.Option5Img,
                        StandardAnwser = b.StandardAnwser,
                        DifficultLevel = b.DifficultLevel ?? 1,
                        ResolutionTips = b.ResolutionTips,
                        QueVersion = b.QueVersion
                    }).ToList();

                if (ques.Count != queNos.Count)
                    return new ServiceResult { success = false, message = "题目不存在" };

                return new ServiceResult { success = true, data = ques };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取题目失败，服务器资源异常，请稍后再试！" };
            }

        }

        public ServiceResult GetQuestionVersion(string paperId)
        {
            try
            {
                var ques = (from a in _dbShare.PaperQuestionsRelation
                            join b in _dbShare.QuestionsInfo on a.QueNo equals b.No
                            where a.PaperID == paperId
                            select new
                            {
                                QueNo = b.No,
                                b.QueVersion
                            }).ToList();

                return new ServiceResult { success = true, data = ques };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取题目版本失败，服务器资源异常，请稍后再试！" };
            }
        }

        public ServiceResult GetRefuseReason(string paperId)
        {
            var reasons = _dbShare.PaperCheckRecord
                .Where(p => p.CheckStatu == (int)CheckStatuDictionary.Refuse && p.PaperID == paperId)
                .Select(p => p.Reason)
                .ToList();
            if (reasons.Count > 0)
                return new ServiceResult { success = true, data = reasons };

            return new ServiceResult { success = false, message = "未查询到原因" };
        }

        public ServiceResult GetReviewQuestions(string paperId)
        {
            try
            {
                var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);
                if (paperInfo == null)
                    return new ServiceResult { success = false, message = "试卷不存在" };


                var queResult = GetQuestions(paperId);

                if (queResult.success)
                {
                    List<QuestionsServiceModel> ques = queResult.data;
                    List<ReviewQuestionServiceModel> reviewQues = ques.ConvertAll(b => new ReviewQuestionServiceModel
                    {
                        QueNo = b.QueNo,
                        QueType = b.QueType,
                        QueContent = b.QueContent,
                        ContentImg = b.ContentImg,
                        Option0 = b.Option0,
                        Option0Img = b.Option0Img,
                        Option1 = b.Option1,
                        Option1Img = b.Option1Img,
                        Option2 = b.Option2,
                        Option2Img = b.Option2Img,
                        Option3 = b.Option3,
                        Option3Img = b.Option3Img,
                        StandardAnwser = b.StandardAnwser,
                        DifficultLevel = b.DifficultLevel,
                        ResolutionTips = b.ResolutionTips,
                    });


                    var sourceQueQuery = _dbShare.Database
                        .SqlQuery<string>($"select QueContent from TotalQuestions_{paperInfo.PaperSpecialtyId}")
                        .ToList()
                        .AsParallel();



                    foreach (var que in reviewQues)
                    {
                        double similarity = 0;
                        sourceQueQuery.ForAll(s =>
                        {
                            if (similarity == 1)
                                return;
                            double tempdouble = LevenshteinDistanceHelper.CompareStrings(s, que.QueContent);
                            if (tempdouble == 1)
                            {
                                similarity = 1;
                                return;
                            }
                            similarity = similarity >= tempdouble ? similarity : tempdouble;
                        });

                        que.Similarity = similarity;
                    }

                    return new ServiceResult { success = true, data = reviewQues };

                }

                return new ServiceResult { success = false, message = "获取试题出错" };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取题目失败，服务器资源异常，请稍后再试！" };
            }

        }

        public ServiceResult GetSchoolsForPaper(string specialtyId)
        {
            try
            {
                var query = from a in _dbShare.PaperInfo
                            join b in _dbShare.UserBindInfo on a.PaperUserId equals b.UserId
                            join c in _dbShare.Base_School on b.SchoolNo equals c.SchoolNo
                            where a.PaperStatu == (int)PaperStatuDictionary.PutOn
                            && b.SpecialtyId == specialtyId
                            group c by new { c.SchoolNo, c.SchoolName } into temp
                            select new SchoolPaperServiceModel
                            {
                                SchoolNo = temp.Key.SchoolNo,
                                SchoolName = temp.Key.SchoolName,
                                PaperCount = temp.Count()
                            };

                return new ServiceResult { success = true, data = query.OrderBy(q => q.SchoolNo).ToList() };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取共享题库失败，服务器资源异常，请稍后再试！" };
            }


        }

        public ServiceResult GetSimilarQuestions(string queNo, double Similarity = 0.3)
        {
            try
            {
                var relation = _dbShare.PaperQuestionsRelation.FirstOrDefault(r => r.QueNo == queNo);
                if (relation == null)
                    return new ServiceResult { success = false, message = "试卷不存在" };

                var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == relation.PaperID);
                if (paperInfo == null)
                    return new ServiceResult { success = false, message = "试卷不存在" };

                if (paperInfo.CheckStatu == 1)
                    return new ServiceResult { success = false, message = "试卷已通过审核" };

                var sourceQueQuery = _dbShare.Database
                            .SqlQuery<TotalQuestionsView>($"select QueContent,OptionA,OptionB,OptionC,OptionD from TotalQuestions_{paperInfo.PaperSpecialtyId}")
                            .ToList()
                            .AsParallel();

                var que = _dbShare.QuestionsInfo.FirstOrDefault(q => q.No == queNo);
                if (que == null)
                    return new ServiceResult { success = false, message = "试题不存在" };

                List<SimilarityQuestionsModel> queModels = new List<SimilarityQuestionsModel>();

                sourceQueQuery.ForAll(s =>
                {
                    double similarity = 0;
                    double tempdouble = LevenshteinDistanceHelper.CompareStrings(s.QueContent, que.QueContent);
                    similarity = similarity >= tempdouble ? similarity : tempdouble;
                    if (similarity >= Similarity)
                    {
                        queModels.Add(new SimilarityQuestionsModel
                        {
                            QueContent = s.QueContent,
                            Option0 = s.OptionA,
                            Option1 = s.OptionB,
                            Option2 = s.OptionC,
                            Option3 = s.OptionD,
                            Similarity = similarity
                        });
                    }
                });
                return new ServiceResult { success = true, data = queModels };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取相似题目失败，服务器资源异常，请稍后再试！" };
            }

        }

        public ServiceResult ModifyQuestions(string paperId, QuestionsServiceModel question)
        {
            var tran = _dbShare.Database.BeginTransaction();
            try
            {
                var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);
                if (paperInfo == null)
                    return new ServiceResult { success = false, message = "试卷不存在" };

                paperInfo.PaperVersion = DateTime.Now.ToNormalStringWithout();
                _dbShare.SaveChanges();

                var que = _dbShare.QuestionsInfo.FirstOrDefault(q => q.No == question.QueNo);
                if (que == null)
                    return new ServiceResult { success = false, message = "题目不存在" };

                if (question.QueType != 0)
                    que.QueType = question.QueType;
                if (question.QueContent != null)
                    que.QueContent = question.QueContent;
                if (question.ContentImg != null && question.ContentImg.Length > 0)
                    que.ContentImg = question.ContentImg;
                if (!string.IsNullOrEmpty(question.Option0))
                    que.Option0 = question.Option0;
                if (question.Option0Img != null && question.Option0Img.Length > 0)
                    que.Option0Img = question.Option0Img;
                if (!string.IsNullOrEmpty(question.Option1))
                    que.Option1 = question.Option1;
                if (question.Option1Img != null && question.Option1Img.Length > 0)
                    que.Option1Img = question.Option1Img;
                if (!string.IsNullOrEmpty(question.Option2))
                    que.Option2 = question.Option2;
                if (question.Option2Img != null && question.Option2Img.Length > 0)
                    que.Option2Img = question.Option2Img;
                if (!string.IsNullOrEmpty(question.Option3))
                    que.Option3 = question.Option3;
                if (question.Option3Img != null && question.Option3Img.Length > 0)
                    que.Option3Img = question.Option3Img;
                if (!string.IsNullOrEmpty(question.StandardAnwser))
                    que.StandardAnwser = question.StandardAnwser;
                if (!string.IsNullOrEmpty(question.ResolutionTips))
                    que.ResolutionTips = question.ResolutionTips;
                que.QueVersion = DateTime.Now.ToNormalStringWithout();
                _dbShare.SaveChanges();

                tran.Commit();
                return new ServiceResult { success = true };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "nono",
                    LogMessage = "修改试题：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                tran.Rollback();

                return new ServiceResult { success = false, message = "修改失败！" };
            }
            finally
            {
                tran.Dispose();
            }
        }

        public ServiceResult PaperUpload(PaperUploadServiceModel paperModel)
        {
            BindType bindType = CheckUserStatu(paperModel.UserToken);

            switch (bindType)
            {
                case BindType.Pass:
                    var uploadModel = Mapper.Map<PaperUploadQueueModel>(paperModel);
                    GlabolDataExe.Instance.AddData(QueueDataType.SharePaperUpload, uploadModel);
                    return new ServiceResult { success = true };

                case BindType.NotBind:
                    return new ServiceResult { success = false, message = "请您先绑定信息。" };

                case BindType.Baned:
                    return new ServiceResult { success = false, message = "您无权限出题，可以联系管理员了解详情" };

                default:
                    return new ServiceResult { success = false, message = "上传失败，请联系管理员" };
            }
        }

        public AliPayStatu QueryOrderStatu(string orderNo)
        {
            return AliPayHelper.Instance.QueryOrder(orderNo);
        }

        public ServiceResult ReviewSharePaper(string userId, string paperId, int checkStatu, string checkReason, string commentDesc, int commentLevel)
        {
            var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);

            if (paperInfo == null)
                return new ServiceResult { success = false, message = "试卷不存在" };

            var tran = _dbShare.Database.BeginTransaction();
            try
            {
                paperInfo.CheckStatu = checkStatu;

                if (checkStatu == (int)CheckStatuDictionary.Pass)
                    paperInfo.PaperStatu = (int)PaperStatuDictionary.PutOn;

                _dbShare.SaveChanges();

                if (checkStatu == (int)CheckStatuDictionary.Refuse)
                {
                    _dbShare.PaperCheckRecord.Add(new DAL.Entity_SharePaper.PaperCheckRecord
                    {
                        CheckStatu = checkStatu,
                        CheckDate = DateTime.Now.ToNormalString(),
                        CheckUserId = userId,
                        PaperID = paperId,
                        Reason = checkReason,
                    });
                    _dbShare.SaveChanges();
                }

                if (checkStatu == (int)CheckStatuDictionary.Pass)
                {
                    _dbShare.CommentRecord.Add(new DAL.Entity_SharePaper.CommentRecord
                    {
                        CommentDate = DateTime.Now.ToNormalString(),
                        CommentDesc = commentDesc,
                        CommentNo = DateTime.Now.ToNormalStringWithout() + new Random().Next(1000, 9999),
                        CommentLevel = commentLevel,
                        CommentUserId = userId,
                        IsAnonymous = 0,
                        IsDelete = 0,
                        PaperID = paperId
                    });
                    _dbShare.SaveChanges();
                }
                tran.Commit();
                return new ServiceResult { success = true };
            }
            catch (Exception ex)
            {
                tran.Rollback();
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = userId,
                    LogMessage = "审核试卷：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);
                return new ServiceResult { success = false, message = "审核出错" };
            }
            finally
            {
                tran.Dispose();
            }
        }

        public ServiceResult UpdatePaperStatu(string userToken, string paperId, int? checkStatu, int? paperStatu)
        {
            try
            {
                if (userToken == null)
                {
                    var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);

                    if (paperInfo == null)
                        return new ServiceResult { success = false, message = "不存在试卷" };


                    if (checkStatu != null)
                        paperInfo.CheckStatu = checkStatu ?? 0;
                    if (paperStatu != null)
                        paperInfo.PaperStatu = paperStatu ?? 0;

                    _dbShare.SaveChanges();

                    return new ServiceResult { success = true };
                }

                BindType bindType;

                var bindInfo = _dbShare.UserBindInfo.FirstOrDefault(u => u.UserToken == userToken);

                if (bindInfo == null)
                    bindType = BindType.NotBind;

                if (bindInfo.UseStatu == 0)
                    bindType = BindType.Baned;

                bindType = BindType.Pass;


                if (bindType == BindType.Pass)
                {
                    var paperInfo = _dbShare.PaperInfo.FirstOrDefault(p => p.PaperID == paperId);

                    if (paperInfo == null)
                        return new ServiceResult { success = false, message = "不存在试卷" };

                    if (paperInfo.PaperUserId != bindInfo.UserId)
                        return new ServiceResult { success = false, message = "无权限" };

                    if (checkStatu != null)
                        paperInfo.CheckStatu = checkStatu ?? 0;
                    if (paperStatu != null)
                        paperInfo.PaperStatu = paperStatu ?? 0;

                    _dbShare.SaveChanges();

                    return new ServiceResult { success = true };
                }


                return new ServiceResult { success = false, message = "无权限" };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = userToken,
                    LogMessage = ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "修改试卷状态出错" };
            }


        }

        public ServiceResult UserBind(TeacherBindServiceModel bindModel)
        {
            BindType bindType = CheckUserStatu(bindModel.UserToken);

            switch (bindType)
            {
                case BindType.Pass | BindType.Baned:
                    return new ServiceResult { success = true, message = "您已经绑定过。" };

                case BindType.NotBind:
                    if (Bind(bindModel))
                        return new ServiceResult { success = true };
                    else
                        return new ServiceResult { success = false, message = "绑定失败，请联系管理员" };

                default:
                    return new ServiceResult { success = false, message = "绑定失败，请联系管理员" };
            }

        }

        private bool Bind(TeacherBindServiceModel bindModel)
        {
            _dbShare.UserBindInfo.Add(new DAL.Entity_SharePaper.UserBindInfo
            {
                LocalLexueid = bindModel.Lexueid,
                SchoolNo = bindModel.SchoolNo,
                SpecialtyId = bindModel.SpecialtyId,
                UseStatu = 1,
                BindDate = DateTime.Now.ToNormalString(),
                PhoneNumber = bindModel.PhoneNumber,
                UserName = bindModel.UserName,
                UserToken = bindModel.UserToken,
                Zhifubao = bindModel.Zhifubao,
                ZhifubaoName = bindModel.ZhifubaoName,
                UserId = ChineseToPinyinHelper.ConvertToSP(bindModel.Lexueid) +
                        "@" + bindModel.SchoolNo + "#" + bindModel.SpecialtyId + "#" + new Random().Next(1000, 9999)
            });
            return _dbShare.SaveChanges() > 0;
        }

    }
}
