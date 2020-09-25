using Api.BLL.Helper;
using Api.BLL.ServiceModel;
using Api.Core;
using Api.Core.AliPay;
using Api.Core.Enum;
using Api.Core.Extensions;
using Api.Core.Logger;
using Api.DAL;
using Api.DAL.DataContext;
using Api.DAL.Entity_Server0905;
using Api.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.Impl
{
    public class RenewService : IRenewService
    {

        private readonly DbServer0905Context _db0905 = DbContextFactory.GetDbServer0905Context();
        public IBaseService _baseService { get; set; }
        public RenewService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public ServiceResult GetRenewInfo(RenewServiceModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.SchoolNo))
                {
                    if (_baseService.GetSchoolSingle(s => s.SchoolNo == model.SchoolNo) == null)
                    {
                        return new ServiceResult { success = false, message = $"学校代码[{model.SchoolNo}]不存在" };
                    }
                }
                else
                {
                    return new ServiceResult { success = false, message = $"学校代码不能为空" };
                }

                if (model.SpecialtyIds == null || model.SpecialtyIds.Count == 0)
                {
                    return new ServiceResult { success = false, message = "请至少选择一个专业" };
                }

                var total_specialty = _db0905.SpecialtyRenewInfo
                        .Where(s => model.SpecialtyIds.Contains(s.SpecialtyId)).ToList();

                double total_price = total_specialty.Sum(s => s.RenewPrice);

                string qrcode_path = string.Empty;
                double discount;

                if (model.RenewType == (int)RenewType.PayToPublic || model.RenewType == (int)RenewType.PayToPrivate)
                {
                    if (model.RenewType == (int)RenewType.PayToPublic)
                        discount = 1;
                    else
                        discount = 0.98;//对私付款打九八折

                    Dictionary<string, double> dicSpecialtyPrice =
                        total_specialty.ToDictionary(k => k.SpecialtyName, v => v.RenewPrice * discount);

                    total_price *= discount;
                    string renewNo = "xf" + model.SchoolNo +
                      DateTime.Now.ToNormalStringWithout() + new Random().Next(1000, 9999);

                    #region 订单写入数据库
                    var record = new RenewRecord
                    {
                        RenewSchoolNo = model.SchoolNo,
                        RenewStatu = 0,
                        RenewDate = DateTime.Now.ToNormalString(),
                        RenewNo = renewNo,
                        RenewPrice = total_price,
                        RenewType = model.RenewType,
                        DownloadCount = 0
                    };
                    var realtions = total_specialty.ConvertAll(s => new RenewRecordRelation
                    {
                        RenewNo = renewNo,
                        SpecialtyId = s.SpecialtyId,
                        RenewPrice = s.RenewPrice * discount,
                        RenewYears = 1
                    });

                    #region 酒店导游一起续费
                    bool has_dy = realtions.Any(s => s.SpecialtyId == "9");
                    bool has_jd = realtions.Any(s => s.SpecialtyId == "10");
                    if (has_dy || has_jd)
                    {
                        if (!has_dy)
                        {
                            realtions.Add(new RenewRecordRelation
                            {
                                RenewNo = renewNo,
                                SpecialtyId = "9",
                                RenewPrice = 24800 * discount,
                                RenewYears = 1
                            });
                        }
                        if (!has_jd)
                        {
                            realtions.Add(new RenewRecordRelation
                            {
                                RenewNo = renewNo,
                                SpecialtyId = "10",
                                RenewPrice = 24800 * discount,
                                RenewYears = 1
                            });
                        }
                    }
                    #endregion


                    if (!CreateRenewOrderRecord(record, realtions))
                    {
                        return new ServiceResult { success = false, message = "创建续费订单失败" };
                    }
                    #endregion

                    #region 生成订单付款二维码
                    if (model.RenewType == (int)RenewType.PayToPublic)
                    {
                        qrcode_path = AliPayHelper.Instance.CreateOrder(renewNo,
                            total_price,
                            "天天乐学题库系统续费",
                            total_specialty.Select(x => x.SpecialtyName).Aggregate((pre, curr) => pre + "、" + curr),
                            total_specialty.Select(x => x.SpecialtyId).Aggregate((pre, curr) => pre + "、" + curr));

                        //将订单号加入后台轮询任务
                        RenewOrderHelper.Instance.orderNos.Add(renewNo);
                    }
                    else
                    {
                        var data = new
                        {
                            s = "money",
                            u = "2088712537825301",
                            a = total_price.ToString(),
                            m = CodeConvert.ConvertUtf8("天天乐学题库系统续费")
                        };
                        string alipay_url = @"alipays://platformapi/startapp?appId=20000123&actionType=scan&biz_data=";

                        qrcode_path = alipay_url + Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    }
                    #endregion

                    RenewInfoServiceModel renew = new RenewInfoServiceModel
                    {
                        RenewNo = renewNo,
                        SpecialtyPrice = dicSpecialtyPrice,
                        QrCode = qrcode_path,
                        Explan = Resource.renew_type_explan
                    };

                    return new ServiceResult { success = true, data = renew };

                }
                else
                {
                    return new ServiceResult { success = false, message = "付款账户不明确" };
                }
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = model.SchoolNo,
                    LogMessage = "获取付款二维码出错：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, data = "服务器异常请稍后再试" };
            }

        }

        private bool CreateRenewOrderRecord(RenewRecord record, List<RenewRecordRelation> relations)
        {
            using (var tran = _db0905.Database.BeginTransaction())
            {
                try
                {
                    int ret = 0;
                    _db0905.RenewRecord.Add(record);
                    _db0905.RenewRecordRelation.AddRange(relations);
                    ret += _db0905.SaveChanges();
                    tran.Commit();
                    return ret > 0;
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = record.RenewSchoolNo,
                        LogMessage = "添加续费订单出错：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                        MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                    }, Log4NetLevel.Error);

                    tran.Rollback();
                    return false;
                }
            }

        }

        public ServiceResult CheckRenewStatu(RenewCheckServiceModel model)
        {
            try
            {
                var statu = AliPayHelper.Instance.QueryOrder(model.RenewNo);
                if (statu == AliPayStatu.Paid)
                {
                    var renew = _db0905.RenewRecord.FirstOrDefault(r => r.RenewNo == model.RenewNo);

                    if (renew == null)
                        return new ServiceResult { success = false, message = "数据异常" };

                    if (renew.RenewStatu != (int)OrderStatuDictionary.Finished)
                    {
                        renew.RenewStatu = (int)OrderStatuDictionary.Finished;
                    }

                    if (renew.RenewLicenseFile == null)
                    {
                        var specialties = (from a in _db0905.SpecialtyRegInfo
                                           join b in _db0905.Base_specialtyType
                                           on a.SpecialtyCode equals b.SpecialtyCode
                                           join c in _db0905.RenewRecordRelation
                                           on b.No equals c.SpecialtyId
                                           where a.SchoolCode == renew.RenewSchoolNo
                                           && c.RenewNo == renew.RenewNo
                                           select a).ToList();

                        var maxDate = DateTime.Parse(specialties.Max(s => s.ExpireTime));
                        string date;
                        if (maxDate.Month > 7)
                        {
                            date = (maxDate.Year + 1) + "-10-30";
                        }
                        else
                        {
                            date = (maxDate.Year) + "-10-30";
                        }

                        foreach (var item in specialties)
                        {
                            item.ExpireTime = date;
                        }

                        var licenseByte = LicenseTool.GenerateLicense(model.CPUID, model.DISKID, model.UUID, DateTime.Parse(date));

                        renew.RenewLicenseFile = licenseByte;
                        _db0905.SaveChanges();

                    }

                    return new ServiceResult { success = true };
                }

                return new ServiceResult { success = false, message = "未查询到您的付款记录，如有疑问请联系QQ 119079525" };
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = model.RenewNo,
                    LogMessage = "检查续费订单出错：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】" + ex.StackTrace,
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "检查订单状态失败，如果您已付款请联系天天乐学工作人员" };
            }
        }

        public ServiceResult GetRenewLicense(LicenseServiceModel model)
        {
            try
            {
                //去最新的续费订单
                var renew = _db0905.RenewRecord
                    .OrderByDescending(r => r.RenewDate)
                    .FirstOrDefault(r => r.RenewStatu == (int)OrderStatuDictionary.Finished && r.RenewSchoolNo == model.SchoolNo);

                if (renew == null)
                    return new ServiceResult { success = true, message = "没有续费订单" };
                if (renew.RenewLicenseFile == null)
                {
                    var specialties = (from a in _db0905.SpecialtyRegInfo
                                       join b in _db0905.Base_specialtyType
                                       on a.SpecialtyCode equals b.SpecialtyCode
                                       join c in _db0905.RenewRecordRelation
                                       on b.No equals c.SpecialtyId
                                       where a.SchoolCode == renew.RenewSchoolNo
                                       && c.RenewNo == renew.RenewNo
                                       select a).ToList();
                    var maxDate = DateTime.Parse(specialties.Max(s => s.ExpireTime));
                    string date;
                    if (maxDate.Month > 7)
                    {
                        date = (maxDate.Year + 1) + "-10-30";
                    }
                    else
                    {
                        date = (maxDate.Year) + "-10-30";
                    }
                    foreach (var item in specialties)
                    {
                        item.ExpireTime = date;
                    }
                    var licenseByte = LicenseTool
                        .GenerateLicense(model.CPUID, model.DISKID, model.UUID, DateTime.Parse(date));
                    renew.RenewLicenseFile = licenseByte;
                    _db0905.SaveChanges();
                    return new ServiceResult { success = true, data = renew.RenewLicenseFile };
                }
                else
                {
                    if (renew.DownloadCount == 0)
                    {
                        renew.DownloadCount += 1;
                        _db0905.SaveChanges();
                        return new ServiceResult { success = true, data = renew.RenewLicenseFile };
                    }
                    else
                    {
                        return new ServiceResult { success = true };
                    }
                }

            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = model.SchoolNo,
                    LogMessage = "获取license出错：" + ex.Message + Environment.NewLine + $"内部异常：【{ex.InnerException?.Message}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return new ServiceResult { success = false, message = "获取license文件出错" };
            }
        }
    }



}
