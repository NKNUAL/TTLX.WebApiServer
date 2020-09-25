using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.DAL.Entity_MonitorSystem;
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
    public class SchoolController : ApiController
    {
        /// <summary>
        /// 通过学校信息查询学校数据上载时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<HttpResultModel> GetUploadTime(SchoolBasicInfos info)
        {
            HttpResultModel ret = new HttpResultModel() { Ret_status = "fail" };
            #region 参数有效性检查
            if (info == null)
            {
                return Json(ret);
            }

            if (info.SchoolName == null)
            {
                return Json(ret);
            }
            if (info.FK_Province == null)
            {
                return Json(ret);
            }
            if (info.SchoolGpCode == null)
            {
                return Json(ret);
            }
            if (info.SchoolCode == null)
            {
                return Json(ret);
            }
            #endregion
            //如果未指定时间，则默认是半夜12点
            string uploadTime = "00:00:00";
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                try
                {
                    string schoolId = string.Empty;
                    SchoolBasicInfos schoolInfo = db.SchoolBasicInfos.Where(sc => sc.SchoolCode == info.SchoolCode && sc.FK_Province == info.FK_Province).FirstOrDefault();
                    if (schoolInfo == null)//判断学校是否是第一次上传数据
                    {
                        schoolId = info.SchoolId = Guid.NewGuid().ToString();
                        db.SchoolBasicInfos.Add(info);
                        db.SchoolDataUploadTime.Add(new SchoolDataUploadTime { SchoolId = schoolId, UploadTime = uploadTime });
                        db.SaveChanges();
                    }
                    else
                    {
                        SchoolDataUploadTime school = db.SchoolDataUploadTime.Where(s => s.SchoolId == schoolInfo.SchoolId).FirstOrDefault();
                        if (school == null)
                        {
                            db.SchoolDataUploadTime.Add(new SchoolDataUploadTime { SchoolId = schoolId, UploadTime = uploadTime });
                            db.SaveChanges();
                        }
                        else
                        {
                            uploadTime = school.UploadTime;
                        }
                    }
                    ret.Ret_status = "success";
                    ret.Ret_message = uploadTime;
                }
                catch (Exception ex)
                {
                    ret.Ret_status = "fail";
                    ret.Ret_message = ex.Message;
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = info.SchoolName,
                        LogMessage = ex.Message,
                        MethodName = "[SchoolController.GetUploadTime()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(ret);
        }

        /// <summary>
        /// 数据上传，将学校的数据上传到数据库
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<HttpResultModel> Upload(MonitorDataMdeol dataModel)
        {
            HttpResultModel ret = new HttpResultModel() { Ret_status = "fail" };
            #region 参数有效性检查
            if (dataModel == null)
                return Json(ret);
            if (dataModel.SchoolInfo == null)
                return Json(ret);
            if (dataModel.SpecialtyData == null || dataModel.SpecialtyData.Count == 0)
                return Json(ret);
            #endregion

            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                SchoolBasicInfos schoolInfo = db.SchoolBasicInfos
                    .Where(sc => sc.SchoolCode == dataModel.SchoolInfo.SchoolCode && sc.FK_Province == dataModel.SchoolInfo.FK_Province).FirstOrDefault();
                if (schoolInfo == null)
                {
                    ret.Ret_message = "未查询到学校信息";
                }
                string uploadTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var tran = db.Database.BeginTransaction();
                try
                {
                    foreach (var spec_data in dataModel.SpecialtyData)
                    {

                        if (spec_data.Value != null)
                        {
                            //保存考试数据
                            if (spec_data.Value.ExamData != null)
                            {
                                foreach (var exam_data in spec_data.Value.ExamData)
                                {
                                    string examGuid = Guid.NewGuid().ToString();
                                    db.UseStatusInfo_Exam.Add(new UseStatusInfo_Exam
                                    {
                                        Id = examGuid,
                                        ExamTime = exam_data.ExamTime,
                                        SchoolId = schoolInfo.SchoolId,
                                        SpecialtyCode = spec_data.Value.SpecialtyCode,
                                        PlanStartTime = exam_data.PlanStartTime,
                                        PlanId = exam_data.PlanId.ToString(),
                                        PlanName = exam_data.PlanName,
                                        TeacherId = exam_data.TeacherId,
                                        TeacherName = exam_data.TeacherName,
                                        RecordUploadTime = uploadTime
                                    });
                                    db.PaperInfo.Add(new PaperInfo
                                    {
                                        PaperCreateTime = exam_data.PaperData.PaperCreateTime,
                                        UseStatusId = examGuid,
                                        PaperId = exam_data.PaperData.PaperId,
                                        PaperName = exam_data.PaperData.PaperName,
                                        PaperType = exam_data.PaperData.PaperType,
                                        PeopleNum = exam_data.PaperData.PeopleNum,
                                        TotalPeopleNum = exam_data.PaperData.TotalPeopleNum
                                    });
                                }
                            }
                            //保存练习数据
                            if (spec_data.Value.ExerciseData != null)
                            {
                                foreach (var exe_data in spec_data.Value.ExerciseData)
                                {
                                    string exeGuid = Guid.NewGuid().ToString();
                                    db.UseStatusInfo_Exercise.Add(new UseStatusInfo_Exercise
                                    {
                                        Id = exeGuid,
                                        SchoolId = schoolInfo.SchoolId,
                                        SpecialtyCode = spec_data.Value.SpecialtyCode,
                                        PlanId = exe_data.Key,
                                        PlanName = exe_data.Value.PlanName,
                                        RecordUploadTime = uploadTime
                                    });
                                    foreach (var paper_data in exe_data.Value.PaperData)
                                    {
                                        db.PaperInfo.Add(new PaperInfo
                                        {
                                            PaperCreateTime = paper_data.PaperCreateTime,
                                            UseStatusId = exeGuid,
                                            PaperId = paper_data.PaperId,
                                            PaperName = paper_data.PaperName,
                                            PaperType = paper_data.PaperType,
                                            PeopleNum = paper_data.PeopleNum,
                                            TotalPeopleNum = paper_data.TotalPeopleNum
                                        });
                                    }
                                }
                            }
                        }
                    }
                    db.SaveChanges();

                    tran.Commit();
                    ret.Ret_status = "success";
                }
                catch (Exception ex)
                {
                    ret.Ret_message = ex.Message;
                    ret.Ret_status = "fail";
                    tran.Rollback();
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = dataModel.SchoolInfo.SchoolName,
                        LogMessage = ex.Message + "\r\n innerException:" + ex.InnerException.Message,
                        MethodName = "[SchoolController.Upload()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(ret);
        }

    }
}
