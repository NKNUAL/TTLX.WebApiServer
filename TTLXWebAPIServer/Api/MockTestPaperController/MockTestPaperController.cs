using Api.BLL;
using Api.Core;
using Api.Core.Logger;
using Api.DAL.DataContext;
using Api.DAL.Entity_MockTestPaper;
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

namespace TTLXWebAPIServer.Api.MockTestPaperController
{
    [RoutePrefix("api/mock")]
    [MockTestAuth]//检查token过滤器
    [WebApiExceptionFilter]
    public class MockTestPaperController : BaseApiController
    {

        public MockTestPaperController(IBaseService baseService) : base(baseService) { }


        private Random _random = new Random();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public  HttpResultModel UserLogin(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.UserId)
                || string.IsNullOrEmpty(loginModel.Password))
            {
                return new HttpResultModel { success = false, message = "参数异常" };
            }

            using (DbUsersContext dbUser = new DbUsersContext())
            {
                var userTable =  dbUser.UserTable
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

                using (DbMockTestPaperContext dbMock = new DbMockTestPaperContext())
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

            var rules = GetRulesByUserId(userId);

            Dictionary<string, QuestionRule> dic_rules = new Dictionary<string, QuestionRule>();

            foreach (var rule in rules)
            {
                if (!dic_rules.ContainsKey(rule.RuleNo))
                {
                    dic_rules.Add(rule.RuleNo, new QuestionRule
                    {
                        RuleNo = rule.RuleNo,
                        SpecialtyId = rule.SpecialtyId,
                        CourseRules = new List<SubRule>(),
                        RuleDesc = rule.RuleDesc,
                        RuleName = rule.RuleName
                    });
                }

                var course = dic_rules[rule.RuleNo].CourseRules.Find(c => c.No == rule.CourseNo);
                if (course == null)
                {
                    course = new SubRule
                    {
                        No = rule.CourseNo,
                        Name = _baseService.GetCourseTypes(rule.SpecialtyId)
                        .ToDictionary(k => k.CourseNo, v => v.CourseName)[rule.CourseNo],
                        QueCount = rule.CoureseQueCount,
                        KnowRules = new List<SubRule>()
                    };
                    dic_rules[rule.RuleNo].CourseRules.Add(course);
                }
                course.KnowRules.Add(new SubRule
                {
                    No = rule.KnowNo,
                    Name = _baseService.GetKnowTypes(rule.SpecialtyId, rule.CourseNo)
                    .ToDictionary(k => k.KnowNo, v => v.KnowName)[rule.KnowNo],
                    QueCount = rule.KnowQueCount
                });

            }

            return new HttpResultModel { success = true, data = dic_rules.Values.ToList() };

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

            using (DbMockTestPaperContext dbMock = new DbMockTestPaperContext())
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

            using (DbMockTestPaperContext dbMock = new DbMockTestPaperContext())
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
                                orderby a.QuestionType, a.OrderIndex
                                select b).ToList();

                    foreach (var que in ques)
                    {
                        if (!dicQues.ContainsKey(que.FK_CourseType))
                            dicQues.Add(que.FK_CourseType, new Dictionary<string, List<QuestionsInfoModel>>());

                        if (!dicQues[que.FK_CourseType].ContainsKey(que.FK_KnowledgePoint))
                            dicQues[que.FK_CourseType].Add(que.FK_KnowledgePoint, new List<QuestionsInfoModel>());

                        dicQues[que.FK_CourseType][que.FK_KnowledgePoint].Add(new QuestionsInfoModel
                        {
                            Anwser = que.StandardAnwser,
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
                    var ques = (from a in dbMock.UserQuestionMockTestPaperQuestionRelation
                                join b in dbMock.Questionsinfo_New
                                on a.QuestionID equals b.No
                                where a.PaperID == paperId.ToString()
                                && new int[] { 1, 2, 3 }.Contains(a.QuestionType ?? 1)//只取选择题
                                && b.sourcedoc == "UserQuestionMockPaper"
                                orderby a.QuestionType, a.OrderIndex
                                select b).ToList();

                    foreach (var que in ques)
                    {
                        if (!dicQues.ContainsKey(que.FK_CourseType))
                            dicQues.Add(que.FK_CourseType, new Dictionary<string, List<QuestionsInfoModel>>());

                        if (!dicQues[que.FK_CourseType].ContainsKey(que.FK_KnowledgePoint))
                            dicQues[que.FK_CourseType].Add(que.FK_KnowledgePoint, new List<QuestionsInfoModel>());

                        dicQues[que.FK_CourseType][que.FK_KnowledgePoint].Add(new QuestionsInfoModel
                        {
                            Anwser = que.StandardAnwser,
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

            using (DbMockTestPaperContext dbMock = new DbMockTestPaperContext())
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
                            QueCount = rule.CourseRules.Sum(c => c.QueCount),
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
                                CourseNo = course.No,
                                QueCount = course.QueCount,
                                RuleNo = ruleNo,
                                CourseRuleNo = courseRuleNo,
                            });

                            foreach (var know in course.KnowRules)
                            {
                                dbMock.UserQuestionRules_Know_Relation.Add(new UserQuestionRules_Know_Relation
                                {
                                    CourseRuleNo = courseRuleNo,
                                    KnowNo = know.No,
                                    QueCount = know.QueCount
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

            Dictionary<string, PutQuestionCourseModel> dicCourse = new Dictionary<string, PutQuestionCourseModel>();

            foreach (var item in ruleItems)
            {
                if (!dicCourse.ContainsKey(item.CourseNo))
                    dicCourse.Add(item.CourseNo, put.Courses.Find(s => s.CourseNo == item.CourseNo));

                var course = dicCourse[item.CourseNo];
                if (course == null)
                    return new HttpResultModel { success = false, message = "请按照出题规则出题" };
                var know = course.Knows.Find(s => s.KnowNo == item.KnowNo);
                if (know == null)
                    return new HttpResultModel { success = false, message = "请按照出题规则出题" };
                if (know.Questions == null || know.Questions.Count != item.KnowQueCount)
                    return new HttpResultModel { success = false, message = "请按照出题规则出题" };
            }

            var uploadModel = Mapper.Map<MockTestPaperQueueModel>(put);
            GlabolDataExe.Instance.AddData(QueueDataType.MockTestPaper, uploadModel);

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
        /// 获取规则
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<RuleModel> GetRulesByUserId(string userId)
        {
            using (DbMockTestPaperContext dbMock = new DbMockTestPaperContext())
            {
                return (from a in dbMock.UserQuestionRules
                        join b in dbMock.UserQuestionRules_Course_Relation on a.RuleNo equals b.RuleNo
                        join c in dbMock.UserQuestionRules_Know_Relation on b.CourseRuleNo equals c.CourseRuleNo
                        where a.IsDelete == 0 && a.UserId == userId
                        orderby a.RuleNo, b.CourseNo, c.KnowNo
                        select new RuleModel
                        {
                            RuleNo = a.RuleNo,
                            RuleName = a.RuleName,
                            RuleDesc = a.RuleDesc,
                            SpecialtyId = a.SpecialtyId,
                            CourseNo = b.CourseNo,
                            CoureseQueCount = b.QueCount,
                            KnowNo = c.KnowNo,
                            KnowQueCount = c.QueCount
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
            using (DbMockTestPaperContext dbMock = new DbMockTestPaperContext())
            {
                return (from a in dbMock.UserQuestionRules
                        join b in dbMock.UserQuestionRules_Course_Relation on a.RuleNo equals b.RuleNo
                        join c in dbMock.UserQuestionRules_Know_Relation on b.CourseRuleNo equals c.CourseRuleNo
                        where a.IsDelete == 0 && a.UserId == userId && a.RuleNo == ruleNo
                        orderby a.RuleNo, b.CourseNo, c.KnowNo
                        select new RuleModel
                        {
                            RuleNo = a.RuleNo,
                            RuleName = a.RuleName,
                            RuleDesc = a.RuleDesc,
                            SpecialtyId = a.SpecialtyId,
                            CourseNo = b.CourseNo,
                            CoureseQueCount = b.QueCount,
                            KnowNo = c.KnowNo,
                            KnowQueCount = c.QueCount
                        }).ToList();
            }
        }
    }
}
