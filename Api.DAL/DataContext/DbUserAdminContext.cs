namespace Api.DAL.DataContext
{
    using Api.Core;
    using Api.DAL.Entity_UserAdmin;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public partial class DbUserAdminContext : DbContext
    {
        public DbUserAdminContext()
            : base()
        {
            Database.Connection.ConnectionString = ConfigTools.GetDBConnString("dbUserAdmin");
            Database.CommandTimeout = 100;
        }

        public virtual DbSet<ExamType> ExamType { get; set; }
        public virtual DbSet<ResultValueTable> ResultValueTable { get; set; }
        public virtual DbSet<Base_Area> Base_Area { get; set; }
        public virtual DbSet<Base_courseType> Base_courseType { get; set; }
        public virtual DbSet<Base_courseType_Computer> Base_courseType_Computer { get; set; }
        public virtual DbSet<Base_courseType_Local> Base_courseType_Local { get; set; }
        public virtual DbSet<Base_knowledgepoint> Base_knowledgepoint { get; set; }
        public virtual DbSet<Base_knowledgepoint_Computer> Base_knowledgepoint_Computer { get; set; }
        public virtual DbSet<Base_Province> Base_Province { get; set; }
        public virtual DbSet<Base_School> Base_School { get; set; }
        public virtual DbSet<Base_specialtyType> Base_specialtyType { get; set; }
        public virtual DbSet<CloudExamRule> CloudExamRule { get; set; }
        public virtual DbSet<CloudExamRule_Computer> CloudExamRule_Computer { get; set; }
        public virtual DbSet<ComposeExamSchema> ComposeExamSchema { get; set; }
        public virtual DbSet<ComposeExamSchemaItem> ComposeExamSchemaItem { get; set; }
        public virtual DbSet<CreateQuestionsUsers> CreateQuestionsUsers { get; set; }
        public virtual DbSet<ExaminationPlan> ExaminationPlan { get; set; }
        public virtual DbSet<ExaminationStudentList> ExaminationStudentList { get; set; }
        public virtual DbSet<ExaminationStudentList_query> ExaminationStudentList_query { get; set; }
        public virtual DbSet<ExamPaper> ExamPaper { get; set; }
        public virtual DbSet<ExamPaper_Computer> ExamPaper_Computer { get; set; }
        public virtual DbSet<ExamPaper_Computer_ProvinceUnion> ExamPaper_Computer_ProvinceUnion { get; set; }
        public virtual DbSet<ExamPaper_ProvinceUnion> ExamPaper_ProvinceUnion { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation> ExamPaperQuestionRelation { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation_Computer> ExamPaperQuestionRelation_Computer { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation_Computer_ProvinceUnion> ExamPaperQuestionRelation_Computer_ProvinceUnion { get; set; }
        public virtual DbSet<ExamPaperQuestionRelation_ProvinceUnion> ExamPaperQuestionRelation_ProvinceUnion { get; set; }
        public virtual DbSet<ExerciseInfo> ExerciseInfo { get; set; }
        public virtual DbSet<ExercisePaper> ExercisePaper { get; set; }
        public virtual DbSet<ExercisePaper_Computer> ExercisePaper_Computer { get; set; }
        public virtual DbSet<ExercisePaperCaozuoTimuRelation> ExercisePaperCaozuoTimuRelation { get; set; }
        public virtual DbSet<ExercisePaperQuestionRelation> ExercisePaperQuestionRelation { get; set; }
        public virtual DbSet<ExercisePaperQuestionRelation_Computer> ExercisePaperQuestionRelation_Computer { get; set; }
        public virtual DbSet<ExercisePaperRelation> ExercisePaperRelation { get; set; }
        public virtual DbSet<ExercisePaperRelation_Computer> ExercisePaperRelation_Computer { get; set; }
        public virtual DbSet<ExerciseScoreResult> ExerciseScoreResult { get; set; }
        public virtual DbSet<GetSmsCodeHistory> GetSmsCodeHistory { get; set; }
        public virtual DbSet<LexueidRelationIDCard> LexueidRelationIDCard { get; set; }
        public virtual DbSet<LexueidRelationIDCard0608> LexueidRelationIDCard0608 { get; set; }
        public virtual DbSet<LocalRegInfo> LocalRegInfo { get; set; }
        public virtual DbSet<LogDetails> LogDetails { get; set; }
        public virtual DbSet<RegInfo> RegInfo { get; set; }
        public virtual DbSet<ReviewQuestionsUsers> ReviewQuestionsUsers { get; set; }
        public virtual DbSet<SaveScoreResult> SaveScoreResult { get; set; }
        public virtual DbSet<SchoolBasicInfo> SchoolBasicInfo { get; set; }
        public virtual DbSet<SchoolPhoneUserLimit> SchoolPhoneUserLimit { get; set; }
        public virtual DbSet<ScoreResultDetail> ScoreResultDetail { get; set; }
        public virtual DbSet<ScoreResultDetial_Caozuoti> ScoreResultDetial_Caozuoti { get; set; }
        public virtual DbSet<SDFDTable> SDFDTable { get; set; }
        public virtual DbSet<Settlement_Questions_Relation> Settlement_Questions_Relation { get; set; }
        public virtual DbSet<SXTDFDRelation> SXTDFDRelation { get; set; }
        public virtual DbSet<SXTTable> SXTTable { get; set; }
        public virtual DbSet<SXTTimuTable> SXTTimuTable { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRoleFunc> SysRoleFunc { get; set; }
        public virtual DbSet<SysRoleFuncRelation> SysRoleFuncRelation { get; set; }
        public virtual DbSet<UpdateUserRoidRecord> UpdateUserRoidRecord { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<UserTable_bak_0730> UserTable_bak_0730 { get; set; }
        public virtual DbSet<UserTable_SchoolUpload> UserTable_SchoolUpload { get; set; }
        public virtual DbSet<WXSessions> WXSessions { get; set; }
        public virtual DbSet<WXSessions_bak_0730> WXSessions_bak_0730 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
