namespace Api.DAL.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Api.DAL.Entity_Users;
    using Api.Core;

    public partial class DbUsersContext : DbContext
    {
        public DbUsersContext()
            : base()
        {
            Database.Connection.ConnectionString = ConfigTools.GetDBConnString("dbUsers");
            Database.CommandTimeout = 100;
        }

        public virtual DbSet<Base_Area> Base_Area { get; set; }
        public virtual DbSet<Base_courseType> Base_courseType { get; set; }
        public virtual DbSet<Base_courseType_Computer> Base_courseType_Computer { get; set; }
        public virtual DbSet<Base_knowledgepoint> Base_knowledgepoint { get; set; }
        public virtual DbSet<Base_knowledgepoint_Computer> Base_knowledgepoint_Computer { get; set; }
        public virtual DbSet<CloudExamRule> CloudExamRule { get; set; }
        public virtual DbSet<CloudExamRule_Computer> CloudExamRule_Computer { get; set; }
        public virtual DbSet<Base_Province> Base_Province { get; set; }
        public virtual DbSet<Base_School> Base_School { get; set; }
        public virtual DbSet<Base_specialtyType> Base_specialtyType { get; set; }
        public virtual DbSet<ErrorQuestionReviewUser> ErrorQuestionReviewUser { get; set; }
        public virtual DbSet<ErrorQuestions> ErrorQuestions { get; set; }
        public virtual DbSet<ErrorQuestionsFeedbackRecord> ErrorQuestionsFeedbackRecord { get; set; }
        public virtual DbSet<ErrorQuestionsModifyRecord> ErrorQuestionsModifyRecord { get; set; }
        public virtual DbSet<LogDetails> LogDetails { get; set; }
        public virtual DbSet<ModifyQuestionAuth> ModifyQuestionAuth { get; set; }
        public virtual DbSet<QuestionReviewMoney> QuestionReviewMoney { get; set; }
        public virtual DbSet<ReviewQuestionAuth> ReviewQuestionAuth { get; set; }
        public virtual DbSet<SysDbType> SysDbType { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRoleMenuRelation> SysRoleMenuRelation { get; set; }
        public virtual DbSet<SysUserRoleRelation> SysUserRoleRelation { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<MockTestPaperAuth> MockTestPaperAuth { get; set; }
        public virtual DbSet<V_QuestionsReviewRecord> V_QuestionsReviewRecord { get; set; }
        public virtual DbSet<V_TotalErrorQuestion> V_TotalErrorQuestion { get; set; }
        public virtual DbSet<V_TotalQuestion> V_TotalQuestion { get; set; }
        public virtual DbSet<SpecialtyBaseRules> SpecialtyBaseRules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
