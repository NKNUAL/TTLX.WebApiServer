using Api.BLL;
using Api.BLL.ServiceModel;
using Api.Core;
using Api.Core.Enum;
using Api.Core.Extensions;
using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.DAL.Entity_MockTestPaper_School;
using Api.Queue;
using Api.Queue.QueueModel;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using TTLXWebAPIServer.Api.MockTestPaperController.Model;
using TTLXWebAPIServer.Api.Model;
using TTLXWebAPIServer.App_Start;
using UserTable_user = Api.DAL.Entity_Users.UserTable;

namespace TTLXWebAPIServer.Api.MockTestPaperController
{
    [RoutePrefix("api/mock")]
    [MockTestAuth]//检查token过滤器
    [WebApiExceptionFilter]
    public class MockTestPaperController : BaseApiController
    {

        public MockTestPaperController(IBaseService baseService) : base(baseService) { }


        private readonly Random _random = new Random();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public HttpResultModel UserLogin(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.UserId)
                || string.IsNullOrEmpty(loginModel.Password))
            {
                return new HttpResultModel { success = false, message = "参数异常" };
            }

            using (DbUsersContext dbUser = new DbUsersContext())
            {
                var userTable = dbUser.UserTable
                    .FirstOrDefault(u => ((u.lexueid == loginModel.UserId && u.UserPassword == loginModel.Password)
                    || (u.KaoHao == loginModel.UserId && u.UserPassword == loginModel.Password))
                    && u.IsDelete == 0 && u.UseState == 1);

                if (userTable == null)
                    return new HttpResultModel { success = false, message = "用户名密码不匹配" };

                var auth = dbUser.MockTestPaperAuth.FirstOrDefault(m => m.Lexueid == userTable.lexueid);

                if (auth == null)
                    return new HttpResultModel { success = false, message = "您没有模拟试卷出题权限！" };

                UserInfoModel info = new UserInfoModel
                {
                    SpecialtyId = auth.SpecialtyType,
                    SpecialtyName = auth.SpecialtyName,
                    UserId = auth.Lexueid,
                    UserName = auth.UserName,
                };

                using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
                {
                    var types = dbMock.QuestionsType
                        .Where(q => q.TypeSpecialty.ToString() == info.SpecialtyId
                        && new int[] { 1, 2, 3 }.Contains(q.TypeID))
                        .ToList();

                    foreach (var item in types)
                    {
                        if (item.TypeID == 1)
                            info.DanxuanScore = item.Score ?? 0;
                        else if (item.TypeID == 2)
                            info.DuoxuanScore = item.Score ?? 0;
                        else if (item.TypeID == 3)
                            info.PanduanScore = item.Score ?? 0;
                    }

                }


                info.Token = TokenHelper.Instance.GenerateMockToken(auth.Lexueid, loginModel.Password, auth.SpecialtyType);

                return new HttpResultModel { success = true, data = info };

            }

        }

        /// <summary>
        /// 获取出题规则
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get/rules/{userId}")]
        public HttpResultModel GetRules(string userId)
        {

            if (string.IsNullOrEmpty(userId))
                return new HttpResultModel { success = false, message = "参数异常" };

            UserTable_user user = null;
            using (DbUsersContext db = new DbUsersContext())
            {
                user = db.UserTable.FirstOrDefault(u => u.lexueid == userId);
            }

            if (user == null)
                return new HttpResultModel { success = false, message = "用户不存在" };

            if (int.Parse(user.FK_Specialty) == (int)SpecialtyType.SU)
            {
                QuestionRule_Nurse rule = new QuestionRule_Nurse
                {
                    RuleNo = Guid.NewGuid().GetGuid(),
                    RuleName = "护理专业出题规则",
                    RuleDesc = "护理专业默认规则",
                    SpecialtyId = user.FK_Specialty,
                    A_ = new List<NurseQuestionRule>()
                };
                rule.A_.Add(new NurseQuestionRule
                {
                    QueCount = 35,
                    SubQueCount = 0,
                    TypeId = 1,
                    TypeName = "A1"
                });
                rule.A_.Add(new NurseQuestionRule
                {
                    QueCount = 10,
                    SubQueCount = 0,
                    TypeId = 2,
                    TypeName = "A1"
                });
                rule.A_.Add(new NurseQuestionRule
                {
                    QueCount = 5,
                    SubQueCount = 3,
                    TypeId = 3,
                    TypeName = "A3"
                });

                return new HttpResultModel { success = true, data = rule };
            }
            else
            {
                var rules = GetRulesByUserId(userId);

                Dictionary<string, QuestionRule> dic_rules = new Dictionary<string, QuestionRule>();

                var dic_course = _baseService.GetCourseTypes(user.FK_Specialty)
                    .ToDictionary(k => k.CourseNo, v => v.CourseName);

                Dictionary<string, Dictionary<string, string>> dic_know = new Dictionary<string, Dictionary<string, string>>();

                foreach (var rule in rules)
                {
                    if (!dic_rules.ContainsKey(rule.RuleNo))
                    {
                        dic_rules.Add(rule.RuleNo, new QuestionRule
                        {
                            RuleNo = rule.RuleNo,
                            SpecialtyId = rule.SpecialtyId,
                            CourseRules = new List<CourseRule>(),
                            RuleDesc = rule.RuleDesc,
                            RuleName = rule.RuleName,
                        });
                    }

                    var course = dic_rules[rule.RuleNo].CourseRules.Find(c => c.CourseNo == rule.CourseNo);
                    if (course == null)
                    {
                        course = new CourseRule
                        {
                            CourseNo = rule.CourseNo,
                            CourseName = dic_course[rule.CourseNo],
                            DanxuanCount = rule.Courese_DanxuanCount,
                            DuoxuanCount = rule.Courese_DuoxuanCount,
                            PanduanCount = rule.Courese_PanduanCount,
                            QueCount = rule.Courese_QueCount,
                            KnowRules = new List<KnowRule>()
                        };
                        dic_rules[rule.RuleNo].CourseRules.Add(course);
                    }

                    if (!dic_know.ContainsKey(rule.CourseNo))
                    {
                        dic_know.Add(rule.CourseNo,
                            _baseService.GetKnowTypes(rule.SpecialtyId, rule.CourseNo)
                            .ToDictionary(k => k.KnowNo, v => v.KnowName));
                    }

                    if (!string.IsNullOrEmpty(rule.KnowNo))
                    {
                        course.KnowRules.Add(new KnowRule
                        {
                            KnowNo = rule.KnowNo,
                            KnowName = dic_know[rule.CourseNo][rule.KnowNo],
                            DanxuanCount = rule.Know_DanxuanCount,
                            DuoxuanCount = rule.Know_DuoxuanCount,
                            PanduanCount = rule.Know_DuoxuanCount
                        });
                    }
                }

                return new HttpResultModel { success = true, data = dic_rules.Values.ToList() };
            }
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("del/rule/{userId}/{ruleNo}")]
        public HttpResultModel DelRule(string userId, string ruleNo)
        {


            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {

                if (dbMock.UserQuestionMockTestPaper.Count(q => q.RuleNo == ruleNo) > 0)
                    return new HttpResultModel { success = false, message = "规则正在被模拟试卷使用，无法删除！" };

                var rule = dbMock.UserQuestionRules.FirstOrDefault(r => r.RuleNo == ruleNo);

                if (rule == null)
                    return new HttpResultModel { success = false, message = "规则不存在" };

                if (rule.UserId != userId)
                    return new HttpResultModel { success = false, message = "无权限删除" };

                using (var tran = dbMock.Database.BeginTransaction())
                {
                    try
                    {
                        string sql = "delete from UserQuestionRules_Know_Relation " +
                            "where CourseRuleNo in " +
                            $"(select CourseRuleNo from UserQuestionRules_Course_Relation where RuleNo='{rule.RuleNo}')";
                        dbMock.Database.ExecuteSqlCommand(sql);
                        sql = $"delete from UserQuestionRules_Course_Relation where RuleNo='{rule.RuleNo}'";
                        dbMock.Database.ExecuteSqlCommand(sql);

                        dbMock.UserQuestionRules.Remove(rule);

                        dbMock.SaveChanges();

                        tran.Commit();

                        return new HttpResultModel { success = true };
                    }
                    catch (Exception ex)
                    {
                        LogContent.Instance.WriteLog(new AppOpLog
                        {
                            LogMessage = "删除规则出错：" + ex.Message,
                            MemberID = userId,
                            MethodName = ""
                        }, Log4NetLevel.Error);

                        tran.Rollback();

                        return new HttpResultModel { success = false, message = "删除出错：" + ex.Message };
                    }
                }


            }
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="editModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit/rule/{userId}")]
        public async Task<HttpResultModel> EditRuleAsync(string userId, QuestionRule updateRule)
        {
            if (string.IsNullOrEmpty(userId))
                return new HttpResultModel { success = false, message = "参数异常" };

            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                if (dbMock.UserQuestionMockTestPaper.Count(q => q.RuleNo == updateRule.RuleNo) > 0)
                    return new HttpResultModel { success = false, message = "规则正在被模拟试卷使用，无法修改！" };

                var rule = dbMock.UserQuestionRules.FirstOrDefault(r => r.RuleNo == updateRule.RuleNo);

                if (rule == null)
                    return new HttpResultModel { success = false, message = "规则不存在" };

                if (rule.UserId != userId)
                    return new HttpResultModel { success = false, message = "无权限修改" };

                using (var tran = dbMock.Database.BeginTransaction())
                {
                    try
                    {
                        rule.RuleName = updateRule.RuleName;
                        rule.RuleDesc = updateRule.RuleDesc;
                        rule.DanxuanCount = updateRule.CourseRules.Sum(c => c.DanxuanCount);
                        rule.DuoxuanCount = updateRule.CourseRules.Sum(c => c.DuoxuanCount);
                        rule.PanduanCount = updateRule.CourseRules.Sum(c => c.PanduanCount);
                        rule.TotalCount = rule.DanxuanCount + rule.DuoxuanCount + rule.PanduanCount;

                        await dbMock.SaveChangesAsync();

                        string sql = "delete from UserQuestionRules_Know_Relation " +
                            "where CourseRuleNo in " +
                            $"(select CourseRuleNo from UserQuestionRules_Course_Relation where RuleNo='{rule.RuleNo}')";
                        await dbMock.Database.ExecuteSqlCommandAsync(sql);
                        sql = $"delete from UserQuestionRules_Course_Relation where RuleNo='{rule.RuleNo}'";
                        await dbMock.Database.ExecuteSqlCommandAsync(sql);

                        foreach (var course in updateRule.CourseRules)
                        {
                            string courseRuleNo = DateTime.Now.ToString("yyyyMMddHHmmss") + _random.Next(1000, 9999);

                            dbMock.UserQuestionRules_Course_Relation.Add(new UserQuestionRules_Course_Relation
                            {
                                CourseNo = course.CourseNo,
                                RuleNo = rule.RuleNo,
                                CourseRuleNo = courseRuleNo,
                                DanxuanCount = course.DanxuanCount,
                                DuoxuanCount = course.DuoxuanCount,
                                PanduanCount = course.PanduanCount,
                                TotalCount = course.QueCount,
                            });

                            await dbMock.SaveChangesAsync();

                            if (course.KnowRules == null)
                                continue;

                            foreach (var know in course.KnowRules)
                            {
                                dbMock.UserQuestionRules_Know_Relation.Add(new UserQuestionRules_Know_Relation
                                {
                                    CourseRuleNo = courseRuleNo,
                                    KnowNo = know.KnowNo,
                                    DanxuanCount = know.DanxuanCount,
                                    DuoxuanCount = know.DuoxuanCount,
                                    PanduanCount = know.PanduanCount,
                                });
                            }

                            await dbMock.SaveChangesAsync();
                        }


                        tran.Commit();

                        return new HttpResultModel { success = true };
                    }
                    catch (Exception ex)
                    {
                        LogContent.Instance.WriteLog(new AppOpLog
                        {
                            LogMessage = "修改规则出错：" + ex.Message,
                            MemberID = userId,
                            MethodName = ""
                        }, Log4NetLevel.Error);

                        tran.Rollback();

                        return new HttpResultModel { success = false, message = ex.Message };
                    }
                }


            }
        }

        /// <summary>
        /// 获取用户模拟试卷
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get/paper/{userId}")]
        public HttpResultModel GetUserMockPaper(string userId, string ruleNo)
        {

            if (string.IsNullOrEmpty(userId))
                return new HttpResultModel { success = false, message = "参数异常" };

            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {

                var query = (from a in dbMock.UserQuestionMockTestPaper
                             join b in dbMock.Base_specialtyType
                             on a.FK_Specialty.ToString() equals b.No
                             join c in dbMock.UserQuestionRules
                             on a.RuleNo equals c.RuleNo
                             where a.CreateUserID == userId
                             select new MockPaperInfo
                             {
                                 SpecialtyId = a.FK_Specialty.ToString(),
                                 SpecialtyName = b.Name,
                                 CreateUserId = a.CreateUserID,
                                 CreateUserName = a.CreateUserName,
                                 PaperCreateDate = a.ExamPaperCreateTime,
                                 PaperName = a.ExamPaperName,
                                 RuleNo = a.RuleNo,
                                 PaperId = a.ExamPaperID,
                                 RuleName = c.RuleName,
                                 DanxuanNum = (int)(a.danxuanNum ?? 0),
                                 DuoxuanNum = (int)(a.duoxuanNum),
                                 PanduanNum = (int)(a.panduanNum)
                             });

                if (!string.IsNullOrEmpty(ruleNo))
                    query = query.Where(m => m.RuleNo == ruleNo);

                List<MockPaperInfo> paperInfos =
                    query.OrderByDescending(q => q.PaperCreateDate).ToList();

                return new HttpResultModel { success = true, data = paperInfos };
            }
        }

        /// <summary>
        /// 获取试卷题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/paperdetail/{paperId}")]
        public HttpResultModel GetPaperDetails(int paperId)
        {
            if (paperId == 0)
                return new HttpResultModel { success = false, message = "参数异常" };

            UserQuestionMockTestPaper paper = null;
            Dictionary<string, Dictionary<string, List<QuestionsInfoModel>>> dicQues =
                    new Dictionary<string, Dictionary<string, List<QuestionsInfoModel>>>();

            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {

                paper = dbMock.UserQuestionMockTestPaper
                   .FirstOrDefault(p => p.ExamPaperID == paperId);

                if (paper == null)
                    return new HttpResultModel { success = false, message = "未查询试卷" };

                if (paper.FK_Specialty == 0)
                {
                    var ques = (from a in dbMock.UserQuestionMockTestPaperQuestionRelation
                                join b in dbMock.Questionsinfo_New_Computer
                                on a.QuestionID equals b.No
                                where a.PaperID == paperId.ToString()
                                && new int[] { 1, 2, 3 }.Contains(a.QuestionType ?? 1)//只取选择题
                                && b.sourcedoc == "UserQuestionMockPaper"
                                orderby a.OrderIndex
                                select b).ToList();

                    foreach (var que in ques)
                    {
                        if (!dicQues.ContainsKey(que.FK_CourseType))
                            dicQues.Add(que.FK_CourseType, new Dictionary<string, List<QuestionsInfoModel>>());

                        if (!dicQues[que.FK_CourseType].ContainsKey(que.FK_KnowledgePoint))
                            dicQues[que.FK_CourseType].Add(que.FK_KnowledgePoint, new List<QuestionsInfoModel>());

                        dicQues[que.FK_CourseType][que.FK_KnowledgePoint].Add(new QuestionsInfoModel
                        {
                            Answer = que.StandardAnwser,
                            NameImg = que.nameImg,
                            No = que.No,
                            Option0 = que.Option0,
                            Option0Img = que.option0Img,
                            Option1 = que.Option1,
                            Option1Img = que.option1Img,
                            Option2 = que.Option2,
                            Option2Img = que.option2Img,
                            Option3 = que.Option3,
                            Option3Img = que.option3Img,
                            Option4 = que.Option4,
                            Option4Img = que.option4Img,
                            Option5 = que.Option5,
                            Option5Img = que.option5Img,
                            QueContent = que.Name,
                            QueType = que.Type ?? 1,
                            ResolutionTips = que.ResolutionTips,
                        });

                    }

                }
                else
                {
                    var aaa = from a in dbMock.UserQuestionMockTestPaperQuestionRelation
                              join b in dbMock.Questionsinfo_New on a.QuestionID equals b.No
                              where a.PaperID == paperId.ToString()
                              && new int[] { 1, 2, 3 }.Contains(a.QuestionType ?? 1)//只取选择题
                              && b.sourcedoc == "UserQuestionMockPaper"
                              orderby a.OrderIndex
                              select b;
                    var ques = (from a in dbMock.UserQuestionMockTestPaperQuestionRelation
                                join b in dbMock.Questionsinfo_New on a.QuestionID equals b.No
                                where a.PaperID == paperId.ToString()
                                && new int[] { 1, 2, 3 }.Contains(a.QuestionType ?? 1)//只取选择题
                                && b.sourcedoc == "UserQuestionMockPaper"
                                orderby a.OrderIndex
                                select b).ToList();

                    foreach (var que in ques)
                    {
                        if (!dicQues.ContainsKey(que.FK_CourseType))
                            dicQues.Add(que.FK_CourseType, new Dictionary<string, List<QuestionsInfoModel>>());

                        if (!dicQues[que.FK_CourseType].ContainsKey(que.FK_KnowledgePoint))
                            dicQues[que.FK_CourseType].Add(que.FK_KnowledgePoint, new List<QuestionsInfoModel>());

                        dicQues[que.FK_CourseType][que.FK_KnowledgePoint].Add(new QuestionsInfoModel
                        {
                            Answer = que.StandardAnwser,
                            NameImg = que.nameImg,
                            No = que.No,
                            Option0 = que.Option0,
                            Option0Img = que.option0Img,
                            Option1 = que.Option1,
                            Option1Img = que.option1Img,
                            Option2 = que.Option2,
                            Option2Img = que.option2Img,
                            Option3 = que.Option3,
                            Option3Img = que.option3Img,
                            Option4 = que.Option4,
                            Option4Img = que.option4Img,
                            Option5 = que.Option5,
                            Option5Img = que.option5Img,
                            QueContent = que.Name,
                            QueType = que.Type ?? 1,
                            ResolutionTips = que.ResolutionTips,
                        });

                    }
                }
            }

            var dic_course = _baseService
                        .GetCourseTypes(paper.FK_Specialty.ToString())
                        .ToDictionary(k => k.CourseNo, v => v.CourseName);

            var courseTree = dicQues.Keys.ToList().ConvertAll(cNo => new MockPaperCourseTreeModel
            {
                CourseNo = cNo,
                CourseName = dic_course[cNo]
            });

            courseTree.ForEach(course =>
            {
                var dic_know = _baseService
                .GetKnowTypes(paper.FK_Specialty.ToString(), course.CourseNo)
                .ToDictionary(k => k.KnowNo, v => v.KnowName);
                course.Knows = dicQues[course.CourseNo].Keys.ToList().ConvertAll(kNo => new MockPaperKnowTreeModel
                {
                    KnowNo = kNo,
                    KnowName = dic_know[kNo],
                });
                course.Knows.ForEach(know =>
                {
                    know.Questions = dicQues[course.CourseNo][know.KnowNo];
                    know.QueCount = dicQues[course.CourseNo][know.KnowNo].Count;
                });
                course.QueCount = course.Knows.Sum(t => t.QueCount);
            });

            return new HttpResultModel { success = true, data = courseTree };
        }

        /// <summary>
        /// 添加出题规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("put/rule/{userId}")]
        public HttpResultModel AddRules(string userId, QuestionRule rule)
        {
            if (string.IsNullOrEmpty(userId))
                return new HttpResultModel { success = false, message = "用户id为空" };
            if (rule == null)
                return new HttpResultModel { success = false, message = "规则信息为空" };

            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                using (var tran = dbMock.Database.BeginTransaction())
                {
                    try
                    {
                        int ret = -1;

                        string ruleNo = DateTime.Now.ToString("yyyyMMddHHmmss") + _random.Next(1000, 9999);

                        dbMock.UserQuestionRules.Add(new UserQuestionRules
                        {
                            SpecialtyId = rule.SpecialtyId,
                            TotalCount = rule.CourseRules.Sum(c => c.QueCount),
                            DanxuanCount = rule.CourseRules.Sum(c => c.DanxuanCount),
                            DuoxuanCount = rule.CourseRules.Sum(c => c.DuoxuanCount),
                            PanduanCount = rule.CourseRules.Sum(c => c.PanduanCount),
                            RuleDesc = rule.RuleDesc,
                            RuleName = rule.RuleName,
                            RuleNo = ruleNo,
                            UserId = userId,
                            IsDelete = 0
                        });
                        ret += dbMock.SaveChanges();

                        foreach (var course in rule.CourseRules)
                        {
                            string courseRuleNo = DateTime.Now.ToString("yyyyMMddHHmmss") + _random.Next(1000, 9999);

                            dbMock.UserQuestionRules_Course_Relation.Add(new UserQuestionRules_Course_Relation
                            {
                                CourseNo = course.CourseNo,
                                DanxuanCount = course.DanxuanCount,
                                DuoxuanCount = course.DuoxuanCount,
                                PanduanCount = course.PanduanCount,
                                TotalCount = course.QueCount,
                                RuleNo = ruleNo,
                                CourseRuleNo = courseRuleNo,
                            });

                            ret += dbMock.SaveChanges();

                            if (course.KnowRules == null)
                                continue;

                            foreach (var know in course.KnowRules)
                            {
                                dbMock.UserQuestionRules_Know_Relation.Add(new UserQuestionRules_Know_Relation
                                {
                                    CourseRuleNo = courseRuleNo,
                                    KnowNo = know.KnowNo,
                                    DanxuanCount = know.DanxuanCount,
                                    DuoxuanCount = know.DuoxuanCount,
                                    PanduanCount = know.PanduanCount
                                });
                            }

                            ret += dbMock.SaveChanges();
                        }

                        tran.Commit();

                        return new HttpResultModel { success = true };
                    }
                    catch (Exception ex)
                    {
                        LogContent.Instance.WriteLog(new AppOpLog
                        {
                            LogMessage = "添加规则出错：" + ex.Message,
                            MemberID = userId,
                            MethodName = "MockTestPaperController::AddRules"
                        }, Log4NetLevel.Error);
                        tran.Rollback();

                        return new HttpResultModel { success = false, message = ex.Message };
                    }
                }
            }
        }

        /// <summary>
        /// 添加模拟试卷
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="put"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("put/paper")]
        public HttpResultModel PutPaper(PutQuestionModel put)
        {
            if (put == null)
                return new HttpResultModel { success = false, message = "题目为空" };

            var ruleItems = GetRulesByRuleNo(put.UserId, put.RuleNo);

            if (ruleItems.Count == 0)
                return new HttpResultModel { success = false, message = "不存在此规则" };


            var uploadModel = Mapper.Map<MockTestPaperQueueModel>(put);
            GlabolDataExe.Instance.AddData(QueueDataType.MockTestPaper, uploadModel);

            return new HttpResultModel { success = true };

        }

        /// <summary>
        /// 修改题目内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("edit/question/{specialtyId}")]
        public HttpResultModel EditPaper(string specialtyId, QuestionsInfoModel que)
        {
            if (string.IsNullOrEmpty(specialtyId) || que is null)
                return new HttpResultModel { success = false, message = "参数异常" };

            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                if (specialtyId == "0")
                {
                    var que_query = dbMock.Questionsinfo_New_Computer
                        .FirstOrDefault(q => q.sourcedoc == "UserQuestionMockPaper" && q.No == que.No);

                    if (que_query == null)
                    {
                        return new HttpResultModel { success = false, message = "试题不存在" };
                    }

                    que_query.Name = que.QueContent;
                    que_query.nameImg = que.NameImg;
                    que_query.Option0 = que.Option0;
                    que_query.option0Img = que.Option0Img;
                    que_query.Option1 = que.Option1;
                    que_query.option1Img = que.Option1Img;
                    que_query.Option2 = que.Option2;
                    que_query.option2Img = que.Option2Img;
                    que_query.Option3 = que.Option3;
                    que_query.option3Img = que.Option3Img;
                    que_query.StandardAnwser = que.Answer;
                    que_query.ResolutionTips = que.ResolutionTips;
                    que_query.Type = que.QueType;
                    que_query.DifficultLevel = que.DifficultLevel;

                    dbMock.SaveChanges();
                }
                else
                {
                    var que_query = dbMock.Questionsinfo_New
                            .FirstOrDefault(q => q.sourcedoc == "UserQuestionMockPaper" && q.No == que.No);

                    if (que_query == null)
                    {
                        return new HttpResultModel { success = false, message = "试题不存在" };
                    }

                    que_query.Name = que.QueContent;
                    que_query.nameImg = que.NameImg;
                    que_query.Option0 = que.Option0;
                    que_query.option0Img = que.Option0Img;
                    que_query.Option1 = que.Option1;
                    que_query.option1Img = que.Option1Img;
                    que_query.Option2 = que.Option2;
                    que_query.option2Img = que.Option2Img;
                    que_query.Option3 = que.Option3;
                    que_query.option3Img = que.Option3Img;
                    que_query.StandardAnwser = que.Answer;
                    que_query.ResolutionTips = que.ResolutionTips;
                    que_query.Type = que.QueType;
                    que_query.DifficultLevel = que.DifficultLevel;

                    dbMock.SaveChanges();
                }
            }
            return new HttpResultModel { success = true };
        }

        /// <summary>
        /// 获取科目
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/course/{specialtyId}")]
        public HttpResultModel GetCourse(string specialtyId)
        {
            if (string.IsNullOrEmpty(specialtyId))
                return new HttpResultModel { success = false, message = "参数异常" };

            var models = _baseService.GetCourseTypes(specialtyId).ConvertAll(c => new KVModel
            {
                Key = c.CourseNo,
                Value = c.CourseName
            });

            return new HttpResultModel { success = true, data = models };
        }

        /// <summary>
        /// 获取知识点
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/course/{specialtyId}/{courseNo}")]
        public HttpResultModel GetKnows(string specialtyId, string courseNo)
        {
            if (string.IsNullOrEmpty(specialtyId))
                return new HttpResultModel { success = false, message = "参数异常" };

            var models = _baseService.GetKnowTypes(specialtyId, courseNo).ConvertAll(c => new KVModel
            {
                Key = c.KnowNo,
                Value = c.KnowName
            });

            return new HttpResultModel { success = true, data = models };
        }

        /// <summary>
        /// 检查题目相似度
        /// </summary>
        /// <param name="checkModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get/similarity")]
        public HttpResultModel GetQuestionSimilarity(QuestionCheckModel checkModel)
        {
            if (checkModel == null
                || string.IsNullOrWhiteSpace(checkModel.SpecialtyId))
                return new HttpResultModel { success = false, message = "参数异常" };

            //代表题目选项为图片，无法对比
            if (string.IsNullOrWhiteSpace(checkModel.SpecialtyId)
                || string.IsNullOrWhiteSpace(checkModel.SpecialtyId)
                || string.IsNullOrWhiteSpace(checkModel.SpecialtyId)
                || string.IsNullOrWhiteSpace(checkModel.SpecialtyId))
            {
                return new HttpResultModel { success = true };
            }

            ParallelQuery<TotalQuestionsView> sourceQueQuery;
            using (DbShareContext dbShare = new DbShareContext())
            {
                sourceQueQuery = dbShare.Database
                            .SqlQuery<TotalQuestionsView>($"select QueContent from TotalQuestions_{checkModel.SpecialtyId}")
                            .AsParallel();

                double Similarity = 0.9;

                List<string> ques = new List<string>();

                Parallel.ForEach(sourceQueQuery, (q, loopState) =>
                {
                    if (!string.IsNullOrWhiteSpace(q.QueContent) && LevenshteinDistanceHelper.CompareStrings(q.QueContent, checkModel.QueContent) >= Similarity)
                    {
                        if (LevenshteinDistanceHelper.CompareStrings(q.OptionA, checkModel.OptionA) >= Similarity
                        || LevenshteinDistanceHelper.CompareStrings(q.OptionB, checkModel.OptionB) >= Similarity
                        || LevenshteinDistanceHelper.CompareStrings(q.OptionC, checkModel.OptionC) >= Similarity
                        || LevenshteinDistanceHelper.CompareStrings(q.OptionD, checkModel.OptionD) >= Similarity)
                        {
                            ques.Add(q.QueContent);
                            loopState.Stop();
                        }
                    }
                });

                return new HttpResultModel { success = true, data = ques.Count > 0 ? ques[0] : null };
            }


        }

        /// <summary>
        /// 获取基础规则
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/baserule/{specialtyId}")]
        public HttpResultModel GetBaseRule(int specialtyId)
        {
            using (DbUsersContext dbUser = new DbUsersContext())
            {
                var baseRule = dbUser.SpecialtyBaseRules.FirstOrDefault(s => s.SpecialtyId == specialtyId);

                if (baseRule == null)
                    return new HttpResultModel { success = false, message = "数据异常，请联系管理员" };

                var rule = new BaseRule
                {
                    SpecialtyId = specialtyId,
                    DanxuanCount = baseRule.DanxuanCount,
                    DuoxuanCount = baseRule.DuoxuanCount,
                    PanduanCount = baseRule.PanduanCount,
                    CourseRules = new List<CourseBaseRule>()
                };

                if (specialtyId == 0)
                {
                    var cloud = dbUser.CloudExamRule_Computer.Where(c => c.ExamSpecialtyID == specialtyId);

                    cloud.ForEach(c =>
                    {
                        rule.CourseRules.Add(new CourseBaseRule
                        {
                            CourseNo = c.ExamCourseID.ToString(),
                            DanxuanCount = c.moduleNumDanxuan ?? 0,
                            DuoxuanCount = c.moduleNumDuoxuan ?? 0,
                            PanduanCount = c.moduleNumPanduan ?? 0
                        });
                    });
                }
                else
                {
                    var cloud = dbUser.CloudExamRule.Where(c => c.ExamSpecialtyID == specialtyId);

                    cloud.ForEach(c =>
                    {
                        rule.CourseRules.Add(new CourseBaseRule
                        {
                            CourseNo = c.ExamCourseID.ToString(),
                            DanxuanCount = c.moduleNumDanxuan ?? 0,
                            DuoxuanCount = c.moduleNumDuoxuan ?? 0,
                            PanduanCount = c.moduleNumPanduan ?? 0
                        });
                    });
                }

                return new HttpResultModel
                {
                    success = true,
                    data = rule
                };
            }







        }

        #region 本地记录相关Api

        /// <summary>
        /// 上传本地记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("upload/record")]
        public async Task<HttpResultModel> UploadLocalRecord(UploadLocalRecord upload)
        {
            LogContent.Instance.WriteLog($"{upload.UserId}上传本地记录", Log4NetLevel.Info);

            if (upload == null)
                return new HttpResultModel { success = false, message = "参数异常" };

            LogContent.Instance.WriteLog($"{upload.UserId}上传本地记录", Log4NetLevel.Info);

            try
            {
                using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
                {
                    if (upload.Papers == null)
                        return new HttpResultModel { success = false, message = "没有需要上传的记录" };

                    foreach (var paper in upload.Papers)
                    {
                        dbMock.LocalPaperRecord.Add(new LocalPaperRecord
                        {
                            CreateUserId = upload.UserId,
                            IsNormal = 0,
                            PaperEditDate = paper.EditDate,
                            RuleNo = paper.RuleNo,
                            PGuid = paper.PGuid
                        });
                        foreach (var course in paper.DicQuestions)
                        {
                            foreach (var know in course.Value)
                            {
                                foreach (var que in know.Value)
                                {
                                    dbMock.LocalPaperQuestions.Add(new LocalPaperQuestions
                                    {
                                        Answer = que.Answer,
                                        CourseNo = course.Key,
                                        DifficultLevel = que.DifficultLevel,
                                        KnowNo = know.Key,
                                        NameImg = que.NameImg,
                                        Option0 = que.Option0,
                                        option0Img = que.Option0Img,
                                        Option1 = que.Option1,
                                        option1Img = que.Option1Img,
                                        Option2 = que.Option2,
                                        option2Img = que.Option2Img,
                                        Option3 = que.Option3,
                                        option3Img = que.Option3Img,
                                        Option4 = que.Option4,
                                        option4Img = que.Option4Img,
                                        Option5 = que.Option5,
                                        option5Img = que.Option5Img,
                                        QGuid = que.No,
                                        QueContent = que.QueContent,
                                        QueType = que.QueType,
                                        ResolutionTips = que.ResolutionTips,
                                    });

                                    dbMock.LocalPaperQuestionRelation.Add(new LocalPaperQuestionRelation
                                    {
                                        PGuid = paper.PGuid,
                                        QGuid = que.No
                                    });
                                }
                            }
                        }
                        await dbMock.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogContent.Instance.WriteLog($"{upload.UserId}上传本地记录失败：" + ex.Message, Log4NetLevel.Error);
                return new HttpResultModel { success = true };
            }

            LogContent.Instance.WriteLog($"{upload.UserId}上传本地记录成功", Log4NetLevel.Info);

            return new HttpResultModel { success = true };
        }

        /// <summary>
        /// 获取编辑记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/record/{userId}/{specialtyId}")]
        public HttpResultModel GetLocalPaperRecords(string userId, int specialtyId)
        {
            if (specialtyId != 5)
            {
                using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
                {
                    var query = from a in dbMock.LocalPaperRecord
                                join b in dbMock.LocalPaperQuestionRelation on a.PGuid equals b.PGuid
                                join c in dbMock.LocalPaperQuestions on b.QGuid equals c.QGuid
                                where a.IsNormal == 0 && a.CreateUserId == userId
                                orderby a.PaperEditDate descending
                                select new
                                {
                                    a.PGuid,
                                    a.RuleNo,
                                    a.PaperEditDate,
                                    Question = c
                                };

                    var ques = query.ToList();

                    Dictionary<string, EditPaperRecord> dic_record = new Dictionary<string, EditPaperRecord>();

                    foreach (var item in ques)
                    {
                        if (!dic_record.ContainsKey(item.PGuid))
                        {
                            dic_record.Add(item.PGuid, new EditPaperRecord
                            {
                                RuleNo = item.RuleNo,
                                PGuid = item.PGuid,
                                EditDate = item.PaperEditDate,
                                DicQuestions = new Dictionary<string, Dictionary<string, List<QuestionsInfoModel>>>()
                            });
                        }

                        if (!dic_record[item.PGuid].DicQuestions.ContainsKey(item.Question.CourseNo))
                        {
                            dic_record[item.PGuid].DicQuestions
                                .Add(item.Question.CourseNo, new Dictionary<string, List<QuestionsInfoModel>>());
                        }

                        if (!dic_record[item.PGuid].DicQuestions[item.Question.CourseNo].ContainsKey(item.Question.KnowNo))
                        {
                            dic_record[item.PGuid].DicQuestions[item.Question.CourseNo]
                                .Add(item.Question.KnowNo, new List<QuestionsInfoModel>());
                        }

                        dic_record[item.PGuid].DicQuestions[item.Question.CourseNo][item.Question.KnowNo]
                            .Add(new QuestionsInfoModel
                            {
                                Answer = item.Question.Answer,
                                NameImg = item.Question.NameImg,
                                Option0 = item.Question.Option0,
                                Option0Img = item.Question.option0Img,
                                Option1 = item.Question.Option1,
                                Option1Img = item.Question.option1Img,
                                Option2 = item.Question.Option2,
                                Option2Img = item.Question.option2Img,
                                Option3 = item.Question.Option3,
                                Option3Img = item.Question.option3Img,
                                Option4 = item.Question.Option4,
                                Option4Img = item.Question.option4Img,
                                Option5 = item.Question.Option5,
                                Option5Img = item.Question.option5Img,
                                QueContent = item.Question.QueContent,
                                QueType = item.Question.QueType,
                                ResolutionTips = item.Question.ResolutionTips,
                                No = item.Question.QGuid
                            });
                    }

                    return new HttpResultModel { success = true, data = dic_record.Values.ToList() };
                }
            }
            else//护理专业
            {

                return new HttpResultModel { success = false, message = "护理专业还未实现" };
            }
        }

        /// <summary>
        /// 保存本地试卷
        /// </summary>
        /// <param name="pGuid"></param>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("save/record/{userId}")]
        public async void SaveLocalPaper(string userId, string pGuid, string ruleNo)
        {
            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                var paper = dbMock.LocalPaperRecord.FirstOrDefault(p => p.PGuid == pGuid);

                if (paper == null)
                {
                    dbMock.LocalPaperRecord.Add(new LocalPaperRecord
                    {
                        IsNormal = 0,
                        PaperEditDate = DateTime.Now.ToNormalString(),
                        PGuid = pGuid,
                        RuleNo = ruleNo,
                        CreateUserId = userId
                    });

                    await dbMock.SaveChangesAsync();
                }
                else
                {
                    paper.PaperEditDate = DateTime.Now.ToNormalString();
                    await dbMock.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// 保存本地题目
        /// </summary>
        [HttpPost]
        [Route("save/question/{pGuid}/{specialtyId}/{courseNo}/{knowNo}")]
        public async void SaveLocalQuestion(string pGuid, int specialtyId, string courseNo, string knowNo, QuestionsInfoModel info)
        {
            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                try
                {
                    var que = dbMock.LocalPaperQuestions.FirstOrDefault(q => q.QGuid == info.No);

                    if (que == null)
                    {
                        dbMock.LocalPaperQuestions.Add(new LocalPaperQuestions
                        {
                            Answer = info.Answer,
                            CourseNo = courseNo,
                            DifficultLevel = info.DifficultLevel,
                            KnowNo = knowNo,
                            NameImg = info.NameImg,
                            Option0 = info.Option0,
                            option0Img = info.Option0Img,
                            Option1 = info.Option1,
                            option1Img = info.Option1Img,
                            Option2 = info.Option2,
                            option2Img = info.Option2Img,
                            Option3 = info.Option3,
                            option3Img = info.Option3Img,
                            QGuid = info.No,
                            QueContent = info.QueContent,
                            QueType = info.QueType,
                            ResolutionTips = info.ResolutionTips,
                        });
                        await dbMock.SaveChangesAsync();

                        dbMock.LocalPaperQuestionRelation.Add(new LocalPaperQuestionRelation
                        {
                            PGuid = pGuid,
                            QGuid = info.No,
                        });

                        await dbMock.SaveChangesAsync();
                    }
                    else
                    {
                        que.Answer = info.Answer;
                        que.Option0 = info.Option0;
                        que.option0Img = info.Option0Img;
                        que.Option1 = info.Option1;
                        que.option1Img = info.Option1Img;
                        que.Option2 = info.Option2;
                        que.option2Img = info.Option2Img;
                        que.Option3 = info.Option3;
                        que.option3Img = info.Option3Img;
                        que.QueContent = info.QueContent;
                        que.NameImg = info.NameImg;
                        que.QueType = info.QueType;
                        que.ResolutionTips = info.ResolutionTips;
                        que.DifficultLevel = info.DifficultLevel;
                        await dbMock.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog("保存本地试题出错：" + ex.Message, Log4NetLevel.Error);
                    string content = string.Format("pGuid:{0}\r\nspecialtyId:{1}\r\ncourseNo:{2}\r\nknowNo:{3}\r\nque:{4}",
                        pGuid, specialtyId, courseNo, knowNo, Newtonsoft.Json.JsonConvert.SerializeObject(info));
                    LogContent.Instance.WriteLog(content, Log4NetLevel.Info);
                }
            }
        }

        /// <summary>
        /// 删除本地编辑记录
        /// </summary>
        /// <param name="pGuid"></param>
        [HttpGet]
        [Route("del/record/{pGuid}")]
        public async void DeleteLocalRecord(string pGuid)
        {
            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                var paper = dbMock.LocalPaperRecord.FirstOrDefault(q => q.PGuid == pGuid);
                if (paper != null)
                {
                    paper.IsNormal = 1;
                    await dbMock.SaveChangesAsync();
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<RuleModel> GetRulesByUserId(string userId)
        {
            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                return (from a in dbMock.UserQuestionRules
                        join b in dbMock.UserQuestionRules_Course_Relation on a.RuleNo equals b.RuleNo
                        join c in dbMock.UserQuestionRules_Know_Relation on b.CourseRuleNo equals c.CourseRuleNo into temp_c
                        from d in temp_c.DefaultIfEmpty()
                        where a.IsDelete == 0 && a.UserId == userId
                        orderby a.RuleNo, b.CourseNo, d.KnowNo
                        select new RuleModel
                        {
                            RuleNo = a.RuleNo,
                            RuleName = a.RuleName,
                            RuleDesc = a.RuleDesc,
                            SpecialtyId = a.SpecialtyId,
                            CourseNo = b.CourseNo,
                            Courese_DanxuanCount = b.DanxuanCount,
                            Courese_DuoxuanCount = b.DuoxuanCount,
                            Courese_PanduanCount = b.PanduanCount,
                            Courese_QueCount = b.TotalCount,
                            KnowNo = d == null ? null : d.KnowNo,
                            Know_DanxuanCount = d == null ? null : d.DanxuanCount,
                            Know_DuoxuanCount = d == null ? null : d.DuoxuanCount,
                            Know_PanduanCount = d == null ? null : d.PanduanCount
                        }).ToList();
            }
        }

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<RuleModel> GetRulesByRuleNo(string userId, string ruleNo)
        {
            using (DbMockTestPaperSchoolContext dbMock = new DbMockTestPaperSchoolContext())
            {
                return (from a in dbMock.UserQuestionRules
                        join b in dbMock.UserQuestionRules_Course_Relation on a.RuleNo equals b.RuleNo
                        join c in dbMock.UserQuestionRules_Know_Relation on b.CourseRuleNo equals c.CourseRuleNo into temp_c
                        from d in temp_c.DefaultIfEmpty()
                        where a.IsDelete == 0 && a.UserId == userId && a.RuleNo == ruleNo
                        orderby a.RuleNo, b.CourseNo, d.KnowNo
                        select new RuleModel
                        {
                            RuleNo = a.RuleNo,
                            RuleName = a.RuleName,
                            RuleDesc = a.RuleDesc,
                            SpecialtyId = a.SpecialtyId,
                            CourseNo = b.CourseNo,
                            Courese_DanxuanCount = b.DanxuanCount,
                            Courese_DuoxuanCount = b.DuoxuanCount,
                            Courese_PanduanCount = b.PanduanCount,
                            Courese_QueCount = b.TotalCount,
                            KnowNo = d == null ? null : d.KnowNo,
                            Know_DanxuanCount = d == null ? null : d.DanxuanCount,
                            Know_DuoxuanCount = d == null ? null : d.DuoxuanCount,
                            Know_PanduanCount = d == null ? null : d.PanduanCount
                        }).ToList();

            }
        }
    }
}
