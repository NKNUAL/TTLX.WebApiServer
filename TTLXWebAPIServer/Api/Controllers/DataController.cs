using Api.Core.Logger;
using Api.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Controllers
{
    [WebApiExceptionFilter]
    public class DataController : ApiController
    {

        /// <summary>
        /// 根据省份查询学校监控日志
        /// </summary>
        /// <param name="FK_province"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<SchoolSimpleDataModel>> GetAll(string FK_province)
        {
            List<SchoolSimpleDataModel> listSchoolData = null;
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                try
                {
                    string filter = string.Empty;
                    string strSql = @"  SELECT info.SchoolId,info.SchoolName,info.FK_Province,bp.TotalName ProvinceName,t1_exam.ExamCount,t2_exam.LastExamTime,t1_exer.ExerciseCount,t2_exer.LastExerciseTime FROM SchoolBasicInfos info 
                                  LEFT JOIN (SELECT SchoolId,COUNT(SchoolId) ExamCount FROM UseStatusInfo_Exam GROUP BY SchoolId) t1_exam 
                                  ON  info.SchoolId = t1_exam.SchoolId
                                  LEFT JOIN (SELECT exam.SchoolId,MAX(exam.PlanStartTime) LastExamTime FROM UseStatusInfo_Exam exam LEFT JOIN PaperInfo p ON exam.Id = p.UseStatusId GROUP BY SchoolId) t2_exam
                                  ON info.SchoolId = t2_exam.SchoolId
                                  LEFT JOIN (SELECT SchoolId,COUNT(SchoolId) ExerciseCount FROM UseStatusInfo_Exercise GROUP BY SchoolId) t1_exer
                                  ON info.SchoolId = t1_exer.SchoolId
                                  LEFT JOIN (SELECT exer.SchoolId,MAX(p.PaperCreateTime) LastExerciseTime FROM UseStatusInfo_Exercise exer LEFT JOIN PaperInfo p ON exer.Id = p.UseStatusId GROUP BY SchoolId) t2_exer
                                  ON info.SchoolId = t2_exer.SchoolId
                                  LEFT JOIN Base_Province bp ON info.FK_Province = bp.No  where 1=1 {0}";
                    if (!string.IsNullOrEmpty(FK_province))
                    {
                        filter = string.Format(" AND info.FK_Province='{0}'", FK_province);
                    }
                    strSql = string.Format(strSql, filter);

                    listSchoolData = db.Database.SqlQuery<SchoolSimpleDataModel>(strSql).ToList();
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "ALL",
                        LogMessage = ex.Message,
                        MethodName = "[DataController.GetAll()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(listSchoolData);
        }

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<ProvinceModel>> GetProvince()
        {
            List<ProvinceModel> provinceModels = null;
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                try
                {
                    string strSql = @"SELECT * FROM (  SELECT bp.No FK_Province,bp.TotalName ProvinceName FROM SchoolBasicInfos info LEFT JOIN Base_Province bp ON info.FK_Province = bp.No) t GROUP BY t.FK_Province,t.ProvinceName";

                    provinceModels = db.Database.SqlQuery<ProvinceModel>(strSql).ToList();
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "ALL",
                        LogMessage = ex.Message,
                        MethodName = "[DataController.GetProvince()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(provinceModels);
        }

        /// <summary>
        /// 获取专业
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<SpecialtyModel>> GetSpecialty()
        {
            List<SpecialtyModel> specialtyModels = new List<SpecialtyModel>();
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                try
                {
                    List<SpecialtyModel> specialtyModels_exam = db.Database.SqlQuery<SpecialtyModel>(@"SELECT t1.SpecialtyCode,bs.Name SpecialtyName FROM (SELECT SpecialtyCode FROM UseStatusInfo_Exam GROUP BY SpecialtyCode) t1
                                                        LEFT JOIN Base_specialtyType bs ON t1.SpecialtyCode = bs.SpecialtyCode").ToList();
                    List<SpecialtyModel> specialtyModels_exercise = db.Database.SqlQuery<SpecialtyModel>(@"SELECT t1.SpecialtyCode,bs.Name SpecialtyName FROM (SELECT SpecialtyCode FROM UseStatusInfo_Exercise GROUP BY SpecialtyCode) t1
                                                        LEFT JOIN Base_specialtyType bs ON t1.SpecialtyCode = bs.SpecialtyCode").ToList();
                    Dictionary<string, SpecialtyModel> pairs = new Dictionary<string, SpecialtyModel>();
                    foreach (var item in specialtyModels_exam)
                    {
                        if (!pairs.ContainsKey(item.SpecialtyCode))
                            pairs.Add(item.SpecialtyCode, item);
                    }
                    foreach (var item in specialtyModels_exercise)
                    {
                        if (!pairs.ContainsKey(item.SpecialtyCode))
                            pairs.Add(item.SpecialtyCode, item);
                    }
                    foreach (var item in pairs)
                    {
                        specialtyModels.Add(item.Value);
                    }
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "ALL",
                        LogMessage = ex.Message,
                        MethodName = "[DataController.GetSpecialty()]"
                    }, Log4NetLevel.Error);
                }
            }
            return Json(specialtyModels);
        }

        /// <summary>
        /// 根据条件查询考试试卷详情
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="startIndex"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ExamPaperMdeol> GetExamPaperData(QueryPaperModel query)
        {
            if (query == null)
                return null;
            StringBuilder sbFilter = new StringBuilder();
            if (query.SchoolId != null)
            {
                sbFilter.AppendFormat(" $ e.SchoolId='{0}' $", query.SchoolId);
            }
            if (query.SpecialtyCode != null)
            {
                sbFilter.AppendFormat(" e.SpecialtyCode='{0}' $", query.SpecialtyCode);
            }

            if (query.PaperType != null)
            {
                sbFilter.AppendFormat(" i.PaperType={0} $", query.PaperType);
            }

            if (query.PlanStartTime != null)
            {
                sbFilter.AppendFormat(" i.PaperCreateTime>='{0}' $", query.PlanStartTime);
            }

            if (query.PlanEndTime != null)
            {
                sbFilter.AppendFormat(" i.PaperCreateTime<='{0}' $", query.PlanEndTime);
            }

            string filter = sbFilter.ToString().TrimEnd('$').Replace("$", "AND");

            string strSql_exam = string.Format(@"SELECT TeacherName,PlanName,PlanStartTime,PaperName,PaperType,TotalPeopleNum,PeopleNum,PaperCreateTime,SpecialtyCode FROM (
                                                SELECT row_number() over(order by t.PlanStartTime desc) as rownumber,* FROM 
                                                (SELECT i.Id,e.TeacherName,e.PlanName,e.PlanStartTime,i.PaperName,e.SpecialtyCode,
                                                i.PaperType,i.TotalPeopleNum,i.PeopleNum,i.PaperCreateTime
                                                FROM UseStatusInfo_Exam e LEFT JOIN PaperInfo i 
                                                ON e.Id=i.UseStatusId WHERE 1=1 {0}) t
                                                ) tt WHERE  rownumber >= {1} and rownumber < {2}", filter, query.StartIndex, query.StartIndex + query.Page);

            ExamPaperMdeol data = new ExamPaperMdeol
            {
                SchoolId = query.SchoolId
            };
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                data.QueryExamCount = db.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(i.id) count FROM UseStatusInfo_Exam e LEFT JOIN PaperInfo i ON e.Id = i.UseStatusId WHERE 1 = 1 {0}", filter)).FirstOrDefault();
                data.QueryExam = db.Database.SqlQuery<QueryExamModel>(strSql_exam).ToList();

            }
            return Json(data);
        }

        /// <summary>
        /// 根据条件查询练习试卷详情
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="startIndex"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ExercisePaperModel> GetExercisePaperData(QueryPaperModel query)
        {
            if (query == null)
                return null;
            StringBuilder sbFilter = new StringBuilder();
            if (query.SchoolId != null)
            {
                sbFilter.AppendFormat(" $ e.SchoolId='{0}' $", query.SchoolId);
            }
            if (query.SpecialtyCode != null)
            {
                sbFilter.AppendFormat(" e.SpecialtyCode='{0}' $", query.SpecialtyCode);
            }

            if (query.PaperType != null)
            {
                sbFilter.AppendFormat(" i.PaperType={0} $", query.PaperType);
            }

            if (query.PlanStartTime != null)
            {
                sbFilter.AppendFormat(" i.PaperCreateTime>='{0}' $", query.PlanStartTime);
            }

            if (query.PlanEndTime != null)
            {
                sbFilter.AppendFormat(" i.PaperCreateTime<='{0}' $", query.PlanEndTime);
            }

            string filter = sbFilter.ToString().TrimEnd('$').Replace("$", "AND");

            string strSql_exercise = string.Format(@"SELECT PlanName,PaperName,PaperType,TotalPeopleNum,PeopleNum,PaperCreateTime,SpecialtyCode FROM (
                                            SELECT row_number() over(order by t.PaperCreateTime desc) as rownumber,* FROM 
                                            (SELECT i.Id,e.PlanName,i.PaperName,e.SpecialtyCode,
                                            i.PaperType,i.TotalPeopleNum,i.PeopleNum,i.PaperCreateTime
                                            FROM UseStatusInfo_Exercise e LEFT JOIN PaperInfo i 
                                            ON e.Id=i.UseStatusId WHERE 1=1 {0}) t
                                            ) tt WHERE rownumber >= {1} and rownumber <= {2}", filter, query.StartIndex, query.StartIndex + query.Page);

            ExercisePaperModel data = new ExercisePaperModel
            {
                SchoolId = query.SchoolId
            };
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                data.QueryExerciseCount = db.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(i.id) count FROM UseStatusInfo_Exercise e LEFT JOIN PaperInfo i ON e.Id = i.UseStatusId WHERE 1 = 1 {0}", filter)).FirstOrDefault();
                data.QueryExercise = db.Database.SqlQuery<QueryExerciseModel>(strSql_exercise).ToList();
            }
            return Json(data);
        }

    }
}
