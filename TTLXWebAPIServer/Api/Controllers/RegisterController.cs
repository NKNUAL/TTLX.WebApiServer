using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.DAL.Entity_Server0905;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Controllers
{
    [WebApiExceptionFilter]
    public class RegisterController : ApiController
    {
        /// <summary>
        /// 查询服务器端所有学校基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<Base_School>> GetAllSchool()
        {
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    var schools = db.Base_School.ToList();
                    return Json(schools);
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.GetAllSchool()]"
                    }, Log4NetLevel.Error);
                    return Json(new List<Base_School>());
                }
            }
        }

        /// <summary>
        /// 获取使用中的省份信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<ProvinceModel>> GetProvince()
        {
            List<ProvinceModel> provinceModels = null;
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    string strSql = @"SELECT t.FK_Province,p.TotalName ProvinceName FROM (SELECT FK_Province FROM Base_School GROUP BY FK_Province) t LEFT JOIN Base_Province p ON t.FK_Province=p.No";

                    provinceModels = db.Database.SqlQuery<ProvinceModel>(strSql).ToList();
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.GetProvince()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(provinceModels);
        }

        /// <summary>
        /// 获取所有省份信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<ProvinceModel>> GetAllProvince()
        {
            List<ProvinceModel> provinceModels = null;
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    string strSql = @"SELECT No FK_Province,TotalName ProvinceName FROM Base_Province";

                    provinceModels = db.Database.SqlQuery<ProvinceModel>(strSql).ToList();
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.GetAllProvince()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(provinceModels);
        }

        /// <summary>
        /// 通过学校代码获取专业注册信息
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<SpecialtyRegInfo>> GetSpecialtyRegInfo(string schoolCode)
        {
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    List<SpecialtyRegInfo> specialtyModels = db.SpecialtyRegInfo.Where(s => s.SchoolCode == schoolCode).ToList();
                    return Json(specialtyModels);
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.GetSpecialtyRegInfo()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(new List<SpecialtyRegInfo>());
        }

        /// <summary>
        /// 获取某省份下所有专业信息
        /// </summary>
        /// <param name="FK_Province"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<SpecialtyModel>> GetAllSpecialty(string FK_Province)
        {
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    List<SpecialtyModel> specialtyModels = db.Database.SqlQuery<SpecialtyModel>("SELECT SpecialtyCode,Name SpecialtyName FROM Base_specialtyType WHERE FK_Province=@FK_Province", new SqlParameter("@FK_Province", FK_Province)).ToList();
                    return Json(specialtyModels);
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.GetAllSpecialty()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(new List<SpecialtyModel>());
        }

        /// <summary>
        /// 学校专业注册
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Register(RegisterModel reg)
        {
            using (DbServer0905Context db = new DbServer0905Context())
            {
                var tran = db.Database.BeginTransaction();
                try
                {
                    List<SpecialtyRegInfo> regInfos = db.SpecialtyRegInfo.Where(s => s.SchoolCode == reg.SchoolCode).ToList();
                    foreach (var item in reg.RegInfos)
                    {
                        if (regInfos.Exists(r => r.SpecialtyCode == item.SpecialtyCode))
                        {
                            var regInfo = regInfos.Find(r => r.SpecialtyCode == item.SpecialtyCode);
                            regInfo.UseStatus = item.UseStatus;
                            regInfo.ExpireTime = item.ExpireTime;
                        }
                        else
                        {
                            db.SpecialtyRegInfo.Add(item);
                        }
                    }
                    db.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.Register()]"
                    }, Log4NetLevel.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查学校代码是否已被使用
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <returns></returns>
        [HttpGet]
        public bool CheckCode(string schoolCode)
        {
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    int count = 0;
                    count = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM Base_School WHERE SchoolNo=@schoolCode", new SqlParameter("schoolCode", schoolCode)).FirstOrDefault();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.CheckCode()]"
                    }, Log4NetLevel.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// 添加学校
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPost]
        public bool PostSchool(Base_School school)
        {
            if (school == null)
            {
                return false;
            }
            using (DbServer0905Context db = new DbServer0905Context())
            {
                try
                {
                    var baseSchool = db.Base_School.Where(s => s.SchoolNo == school.SchoolNo).FirstOrDefault();
                    if (baseSchool == null)
                    {
                        db.Base_School.Add(school);
                    }
                    else
                    {
                        baseSchool.GPCode = school.GPCode;
                    }
                    int ret = db.SaveChanges();
                    return ret > 0;
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "SystemTool",
                        LogMessage = ex.Message,
                        MethodName = "[RegisterController.PostSchool()]"
                    }, Log4NetLevel.Error);
                    return false;
                }
            }
        }

    }
}
