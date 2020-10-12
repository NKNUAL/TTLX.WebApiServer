namespace Api.DAL.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Api.Core;
    using Api.DAL.Entity_MockTestPaper;

    public partial class DbMockTestPaperContext : DbContext
    {
        public DbMockTestPaperContext() : base()
        {
            Database.Connection.ConnectionString = ConfigTools.GetDBConnString("dbMockTestPaper");
            Database.CommandTimeout = 100;
        }

        public virtual DbSet<ExamType> ExamType { get; set; }
        public virtual DbSet<PaperCaozuoTimuRelation> PaperCaozuoTimuRelation { get; set; }
        public virtual DbSet<ResultValueTable> ResultValueTable { get; set; }
        public virtual DbSet<Base_courseType> Base_courseType { get; set; }
        public virtual DbSet<Base_courseType_Computer> Base_courseType_Computer { get; set; }
        public virtual DbSet<Base_knowledgepoint> Base_knowledgepoint { get; set; }
        public virtual DbSet<Base_knowledgepoint_Computer> Base_knowledgepoint_Computer { get; set; }
        public virtual DbSet<Base_specialtyType> Base_specialtyType { get; set; }
        public virtual DbSet<CloudExamRule> CloudExamRule { get; set; }
        public virtual DbSet<CloudExamRule_Computer> CloudExamRule_Computer { get; set; }
        public virtual DbSet<ComposeExamSchemaItem> ComposeExamSchemaItem { get; set; }
        public virtual DbSet<CreateQuestionsUsers> CreateQuestionsUsers { get; set; }
        public virtual DbSet<ExaminationPlan> ExaminationPlan { get; set; }
        public virtual DbSet<ExaminationStudentList> ExaminationStudentList { get; set; }
        public virtual DbSet<ExamPaper> ExamPaper { get; set; }
        public virtual DbSet<ExamPaper_Computer> ExamPaper_Computer { get; set; }
        public virtual DbSet<ExamPaper_Computer_ProvinceUnion> ExamPaper_Computer_ProvinceUnion { get; set; }
        public virtual DbSet<ExamPaper_ProvinceUnion> ExamPaper_ProvinceUnion { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation> ExamPaperQuestionRelation { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation_Computer> ExamPaperQuestionRelation_Computer { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation_Computer_ProvinceUnion> ExamPaperQuestionRelation_Computer_ProvinceUnion { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation_ProvinceUnion> ExamPaperQuestionRelation_ProvinceUnion { get; set; }
        public virtual DbSet<ExercisePaper> ExercisePaper { get; set; }
        public virtual DbSet<ExercisePaper_Computer> ExercisePaper_Computer { get; set; }
        public virtual DbSet<ExercisePaperCaozuoTimuRelation> ExercisePaperCaozuoTimuRelation { get; set; }
        public virtual DbSet<ExercisePaperQuestionRelation> ExercisePaperQuestionRelation { get; set; }
        public virtual DbSet<ExercisePaperQuestionRelation_Computer> ExercisePaperQuestionRelation_Computer { get; set; }
        public virtual DbSet<ExercisePaperRelation> ExercisePaperRelation { get; set; }
        public virtual DbSet<ExercisePaperRelation_Computer> ExercisePaperRelation_Computer { get; set; }
        public virtual DbSet<ExerciseScoreResult> ExerciseScoreResult { get; set; }
        public virtual DbSet<GetSmsCodeHistory> GetSmsCodeHistory { get; set; }
        public virtual DbSet<LocalRegInfo> LocalRegInfo { get; set; }
        public virtual DbSet<MockTestPaper> MockTestPaper { get; set; }
        public virtual DbSet<MockTestPaper_Computer> MockTestPaper_Computer { get; set; }
        public virtual DbSet<MockTestPaperQuestionRelation> MockTestPaperQuestionRelation { get; set; }
        public virtual DbSet<MockTestPaperQuestionRelation_Computer> MockTestPaperQuestionRelation_Computer { get; set; }
        public virtual DbSet<MockTestPaperRule> MockTestPaperRule { get; set; }
        public virtual DbSet<MockTestPaperRule1> MockTestPaperRule1 { get; set; }
        public virtual DbSet<PaperCaozuoTimuRelation_ProvinceUnion> PaperCaozuoTimuRelation_ProvinceUnion { get; set; }
        public virtual DbSet<ProvinceUnionExamInfo> ProvinceUnionExamInfo { get; set; }
        public virtual DbSet<ProvinceUnionExamStudentList> ProvinceUnionExamStudentList { get; set; }
        public virtual DbSet<QuestionBankInfo> QuestionBankInfo { get; set; }
        public virtual DbSet<Questionsinfo_New> Questionsinfo_New { get; set; }
        public virtual DbSet<Questionsinfo_New_Computer> Questionsinfo_New_Computer { get; set; }
        public virtual DbSet<Questionsinfo_New_Computer_ProvinceUnion> Questionsinfo_New_Computer_ProvinceUnion { get; set; }
        public virtual DbSet<Questionsinfo_New_DZSW> Questionsinfo_New_DZSW { get; set; }
        public virtual DbSet<Questionsinfo_New_ProvinceUnion> Questionsinfo_New_ProvinceUnion { get; set; }
        public virtual DbSet<Questionsinfo_Recommend> Questionsinfo_Recommend { get; set; }
        public virtual DbSet<Questionsinfo_Recommend_Settlement> Questionsinfo_Recommend_Settlement { get; set; }
        public virtual DbSet<QuestionsReviewRecord> QuestionsReviewRecord { get; set; }
        public virtual DbSet<QuestionsType> QuestionsType { get; set; }
        public virtual DbSet<ReviewQuestionsUsers> ReviewQuestionsUsers { get; set; }
        public virtual DbSet<ScoreResultDetail> ScoreResultDetail { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
