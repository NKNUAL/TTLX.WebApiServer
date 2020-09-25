namespace Api.DAL.DataContext
{
    using Api.Core;
    using Api.DAL.Entity_TTLXExamSystem3;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public partial class DbSystem3Context : DbContext
    {
        public DbSystem3Context()
            : base()
        {
            Database.Connection.ConnectionString = ConfigTools.GetDBConnString("dbSystem3");
            Database.CommandTimeout = 100;
        }

        public virtual DbSet<ExamType> ExamType { get; set; }
        public virtual DbSet<PaperCaozuoTimuRelation> PaperCaozuoTimuRelation { get; set; }
        public virtual DbSet<PaperCaozuoTimuRelation_Cloud> PaperCaozuoTimuRelation_Cloud { get; set; }
        public virtual DbSet<ResultValueTable> ResultValueTable { get; set; }
        public virtual DbSet<TimuDetail> TimuDetail { get; set; }
        public virtual DbSet<TimuDetailScorePoint> TimuDetailScorePoint { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<Base_courseType> Base_courseType { get; set; }
        public virtual DbSet<Base_courseType_Computer> Base_courseType_Computer { get; set; }
        public virtual DbSet<Base_knowledgepoint> Base_knowledgepoint { get; set; }
        public virtual DbSet<Base_knowledgepoint_Computer> Base_knowledgepoint_Computer { get; set; }
        public virtual DbSet<Base_Province> Base_Province { get; set; }
        public virtual DbSet<Base_specialtyType> Base_specialtyType { get; set; }
        public virtual DbSet<CaozuoSXTRelation> CaozuoSXTRelation { get; set; }
        public virtual DbSet<CloudExamRule> CloudExamRule { get; set; }
        public virtual DbSet<CreateQuestionsUsers> CreateQuestionsUsers { get; set; }
        public virtual DbSet<ExaminationPlan> ExaminationPlan { get; set; }
        public virtual DbSet<ExaminationStudentList> ExaminationStudentList { get; set; }
        public virtual DbSet<ExamPaper> ExamPaper { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation> ExamPaperQuestionRelation { get; set; }
        public virtual DbSet<ExerciseInfo> ExerciseInfo { get; set; }
        public virtual DbSet<ExercisePaper> ExercisePaper { get; set; }
        public virtual DbSet<ExercisePaperQuestionRelation> ExercisePaperQuestionRelation { get; set; }
        public virtual DbSet<ExercisePaperRelation> ExercisePaperRelation { get; set; }
        public virtual DbSet<GetSmsCodeHistory> GetSmsCodeHistory { get; set; }
        public virtual DbSet<QuestionReviewDictionary> QuestionReviewDictionary { get; set; }
        public virtual DbSet<QuestionReviewDictionary3> QuestionReviewDictionary3 { get; set; }
        public virtual DbSet<QuestionsDeductRecord> QuestionsDeductRecord { get; set; }
        public virtual DbSet<Questionsinfo> Questionsinfo { get; set; }
        public virtual DbSet<Questionsinfo_Computer> Questionsinfo_Computer { get; set; }
        public virtual DbSet<QuestionsInfo_Computer_Ori> QuestionsInfo_Computer_Ori { get; set; }
        public virtual DbSet<Questionsinfo_New> Questionsinfo_New { get; set; }
        public virtual DbSet<Questionsinfo_New_Computer_MockTestPaper> Questionsinfo_New_Computer_MockTestPaper { get; set; }
        public virtual DbSet<Questionsinfo_New_MockTestPaper> Questionsinfo_New_MockTestPaper { get; set; }
        public virtual DbSet<Questionsinfo_Ori> Questionsinfo_Ori { get; set; }
        public virtual DbSet<Questionsinfo_Recommend> Questionsinfo_Recommend { get; set; }
        public virtual DbSet<Questionsinfo_Recommend_Review> Questionsinfo_Recommend_Review { get; set; }
        public virtual DbSet<Questionsinfo_Recommend_Settlement> Questionsinfo_Recommend_Settlement { get; set; }
        public virtual DbSet<QuestionsReviewRecord> QuestionsReviewRecord { get; set; }
        public virtual DbSet<RegInfo> RegInfo { get; set; }
        public virtual DbSet<Review_Questions_Relation> Review_Questions_Relation { get; set; }
        public virtual DbSet<ReviewQuestionsUsers> ReviewQuestionsUsers { get; set; }
        public virtual DbSet<SaveScoreResult> SaveScoreResult { get; set; }
        public virtual DbSet<SDFDTable> SDFDTable { get; set; }
        public virtual DbSet<Settlement_Questions_AddCut> Settlement_Questions_AddCut { get; set; }
        public virtual DbSet<Settlement_Questions_Relation> Settlement_Questions_Relation { get; set; }
        public virtual DbSet<SXTDFDRelation> SXTDFDRelation { get; set; }
        public virtual DbSet<SXTTable> SXTTable { get; set; }
        public virtual DbSet<SXTTimuTable> SXTTimuTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamType>()
                .Property(e => e.FK_SpecialtyType)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.TimuSteps)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.Filehouzhui)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.SC1houzui)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.SC2houzui)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.SC3houzui)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.TimuLevel)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.FK_SpecialtyType)
                .IsFixedLength();

            modelBuilder.Entity<TimuDetail>()
                .Property(e => e.UserID)
                .IsFixedLength();

            modelBuilder.Entity<UserTable>()
                .Property(e => e.lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.IDcard)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.FK_SpecialtyName)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.FK_School)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserDesc)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserClass)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.nameDesc)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType>()
                .Property(e => e.FK_Province)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType_Computer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType_Computer>()
                .Property(e => e.FK_Province)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType_Computer>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_courseType_Computer>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint_Computer>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint_Computer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint_Computer>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Base_knowledgepoint_Computer>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Province>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Province>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Province>()
                .Property(e => e.TotalName)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.FK_Province)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.DanxuanScore)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.DuoxuanScore)
                .IsUnicode(false);

            modelBuilder.Entity<Base_specialtyType>()
                .Property(e => e.PanduanScore)
                .IsUnicode(false);

            modelBuilder.Entity<CloudExamRule>()
                .Property(e => e.moduleName)
                .IsUnicode(false);

            modelBuilder.Entity<CloudExamRule>()
                .Property(e => e.knowledgeList)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.Lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.SchoolName)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.FK_SpecialtyName)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.UserPwd)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.Zhifubao)
                .IsUnicode(false);

            modelBuilder.Entity<CreateQuestionsUsers>()
                .Property(e => e.CourseModule)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationPlan>()
                .Property(e => e.KsjhName)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationPlan>()
                .Property(e => e.SpecialtyName)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationPlan>()
                .Property(e => e.StartTime)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationPlan>()
                .Property(e => e.ClassDesc)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationStudentList>()
                .Property(e => e.StudentId)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationStudentList>()
                .Property(e => e.StudentName)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationStudentList>()
                .Property(e => e.StudentPCAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationStudentList>()
                .Property(e => e.ExamRstartTime)
                .IsUnicode(false);

            modelBuilder.Entity<ExaminationStudentList>()
                .Property(e => e.ExamEndTime)
                .IsUnicode(false);

            modelBuilder.Entity<ExamPaper>()
                .Property(e => e.ExamPaperName)
                .IsUnicode(false);

            modelBuilder.Entity<ExamPaper>()
                .Property(e => e.ExamPaperCreateTime)
                .IsUnicode(false);

            modelBuilder.Entity<ExamPaper>()
                .Property(e => e.CreateUserID)
                .IsUnicode(false);

            modelBuilder.Entity<ExamPaper>()
                .Property(e => e.CreateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<ExamPaperQuestionRelation>()
                .Property(e => e.QuestionID)
                .IsUnicode(false);

            modelBuilder.Entity<ExerciseInfo>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<ExerciseInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ExerciseInfo>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<ExerciseInfo>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<ExerciseInfo>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<ExerciseInfo>()
                .Property(e => e.ClassDesc)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaper>()
                .Property(e => e.ExamPaperName)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaper>()
                .Property(e => e.CreateTime)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaperQuestionRelation>()
                .Property(e => e.QuestionID)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaperQuestionRelation>()
                .Property(e => e.Point)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaperRelation>()
                .Property(e => e.ExerciseNo)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaperRelation>()
                .Property(e => e.PaperID)
                .IsUnicode(false);

            modelBuilder.Entity<ExercisePaperRelation>()
                .Property(e => e.PaperName)
                .IsUnicode(false);

            modelBuilder.Entity<GetSmsCodeHistory>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<GetSmsCodeHistory>()
                .Property(e => e.ClientEndPoint)
                .IsUnicode(false);

            modelBuilder.Entity<GetSmsCodeHistory>()
                .Property(e => e.CreateTime)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionReviewDictionary>()
                .Property(e => e.ReviewValue)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionReviewDictionary>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionReviewDictionary3>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsDeductRecord>()
                .Property(e => e.SettleNo)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsDeductRecord>()
                .Property(e => e.Lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsDeductRecord>()
                .Property(e => e.QueNo)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsDeductRecord>()
                .Property(e => e.DeductReason)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.ParentNo)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Point)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option0)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option1)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option2)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option3)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option4)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option5)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option6)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option7)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option8)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option9)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.ResolutionTips)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_Province)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_Chapter)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_KnowledgePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_Creater)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.FK_Crew)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.UpdateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.StandardAnwserText)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.Option0Text)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.StandardAnwser)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.StandardMultiAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic1Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic2Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic3Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic4Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic5Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic6Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic7Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic8Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic9Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.pic10Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo>()
                .Property(e => e.sourcedoc)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.ParentNo)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Point)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option0)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option1)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option2)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option3)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option4)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option5)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option6)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option7)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option8)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option9)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.ResolutionTips)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_Province)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_Chapter)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_KnowledgePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_Creater)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.FK_Crew)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.UpdateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.StandardAnwserText)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.Option0Text)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.StandardAnwser)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.StandardMultiAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic1Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic2Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic3Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic4Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic5Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic6Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic7Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic8Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic9Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.pic10Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Computer>()
                .Property(e => e.sourcedoc)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.StandardMultiAnswer)
                .IsFixedLength();

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.ReviewUserID)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.ReviewUserName)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.ReviewTime)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.Lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.Re_Review_Date)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsInfo_Computer_Ori>()
                .Property(e => e.Re_Review_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.ParentNo)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Point)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option0)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option1)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option2)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option3)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option4)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option5)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option6)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option7)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option8)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option9)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.ResolutionTips)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_Province)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_Chapter)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_KnowledgePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_Creater)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.FK_Crew)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.UpdateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.StandardAnwserText)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.Option0Text)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.StandardAnwser)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.StandardMultiAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic1Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic2Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic3Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic4Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic5Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic6Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic7Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic8Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic9Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.pic10Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New>()
                .Property(e => e.sourcedoc)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option0)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option1)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option2)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option3)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option4)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option5)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option6)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option7)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option8)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.Option9)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.ResolutionTips)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.FK_KnowledgePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.StandardAnwser)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.StandardMultiAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.sourcedoc)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.CreateUserPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.CreateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.CreateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_Computer_MockTestPaper>()
                .Property(e => e.VersionFlag)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option0)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option1)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option2)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option3)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option4)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option5)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option6)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option7)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option8)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.Option9)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.ResolutionTips)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.FK_SpecialtyType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.FK_CourseType)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.FK_KnowledgePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.StandardAnwser)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.StandardMultiAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.sourcedoc)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.CreateUserPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.CreateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.CreateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_New_MockTestPaper>()
                .Property(e => e.VersionFlag)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.StandardMultiAnswer)
                .IsFixedLength();

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.ReviewUserID)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.ReviewUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.ReviewTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.Lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.Re_Review_Date)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Ori>()
                .Property(e => e.Re_Review_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.QueNo)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.QueContent)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.OptionA)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.OptionB)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.OptionC)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.OptionD)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.OptionE)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.OptionF)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.StandardAnwser)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.ResolutionTips)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.FK_Specialty)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.FK_Course)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.FK_KnowledgePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.KnowledgePointName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.CreateUserPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.CreateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.CreateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend>()
                .Property(e => e.NoPassReason)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Review>()
                .Property(e => e.SettleNo)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Review>()
                .Property(e => e.SettleTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Review>()
                .Property(e => e.ReviewUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Review>()
                .Property(e => e.ReviewUserPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Review>()
                .Property(e => e.ReviewUserZhifubao)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Review>()
                .Property(e => e.PorcessTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Settlement>()
                .Property(e => e.SettleNo)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Settlement>()
                .Property(e => e.SettleTime)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Settlement>()
                .Property(e => e.CreateUserName)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Settlement>()
                .Property(e => e.CreateUserPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Settlement>()
                .Property(e => e.CreateUserZhifubao)
                .IsUnicode(false);

            modelBuilder.Entity<Questionsinfo_Recommend_Settlement>()
                .Property(e => e.PorcessTime)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsReviewRecord>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsReviewRecord>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsReviewRecord>()
                .Property(e => e.QueNo)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionsReviewRecord>()
                .Property(e => e.ReviewTime)
                .IsUnicode(false);

            modelBuilder.Entity<RegInfo>()
                .Property(e => e.SpecialtyNo)
                .IsUnicode(false);

            modelBuilder.Entity<RegInfo>()
                .Property(e => e.SpecialtyName)
                .IsUnicode(false);

            modelBuilder.Entity<RegInfo>()
                .Property(e => e.registerCode)
                .IsUnicode(false);

            modelBuilder.Entity<RegInfo>()
                .Property(e => e.GpCode)
                .IsUnicode(false);

            modelBuilder.Entity<RegInfo>()
                .Property(e => e.CanUse)
                .IsUnicode(false);

            modelBuilder.Entity<RegInfo>()
                .Property(e => e.VerifyCode)
                .IsUnicode(false);

            modelBuilder.Entity<Review_Questions_Relation>()
                .Property(e => e.SettleNo)
                .IsUnicode(false);

            modelBuilder.Entity<Review_Questions_Relation>()
                .Property(e => e.QueNo)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.Lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.SchoolName)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.FK_SpecialtyName)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.FK_CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<ReviewQuestionsUsers>()
                .Property(e => e.UserPwd)
                .IsUnicode(false);

            modelBuilder.Entity<SaveScoreResult>()
                .Property(e => e.caozuo1Score)
                .IsFixedLength();

            modelBuilder.Entity<SaveScoreResult>()
                .Property(e => e.caozuo2Score)
                .IsFixedLength();

            modelBuilder.Entity<SaveScoreResult>()
                .Property(e => e.caozuo3Score)
                .IsFixedLength();

            modelBuilder.Entity<SaveScoreResult>()
                .Property(e => e.caozuo4Score)
                .IsFixedLength();

            modelBuilder.Entity<SaveScoreResult>()
                .Property(e => e.caozuo5Score)
                .IsFixedLength();

            modelBuilder.Entity<SaveScoreResult>()
                .Property(e => e.caozuo6Score)
                .IsFixedLength();

            modelBuilder.Entity<Settlement_Questions_AddCut>()
                .Property(e => e.Lexueid)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement_Questions_AddCut>()
                .Property(e => e.SettleNo)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement_Questions_AddCut>()
                .Property(e => e.QueNo)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement_Questions_Relation>()
                .Property(e => e.SettleNo)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement_Questions_Relation>()
                .Property(e => e.QueNo)
                .IsUnicode(false);
        }
    }
}
