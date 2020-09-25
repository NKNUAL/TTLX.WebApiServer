namespace Api.DAL.DataContext
{
    using Api.Core;
    using Api.DAL.Entity_SharePaper;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public partial class DbShareContext : DbContext
    {
        public DbShareContext()
            : base()
        {
            Database.Connection.ConnectionString = ConfigTools.GetDBConnString("dbSharePaper");
            Database.CommandTimeout = 100;
        }

        public virtual DbSet<Base_School> Base_School { get; set; }
        public virtual DbSet<Base_specialtyType> Base_specialtyType { get; set; }
        public virtual DbSet<CheckStatuDictionary> CheckStatuDictionary { get; set; }
        public virtual DbSet<CommentRecord> CommentRecord { get; set; }
        public virtual DbSet<OrderRecord> OrderRecord { get; set; }
        public virtual DbSet<OrderStatuDictionary> OrderStatuDictionary { get; set; }
        public virtual DbSet<PaperInfo> PaperInfo { get; set; }
        public virtual DbSet<PaperStatuDictionary> PaperStatuDictionary { get; set; }
        public virtual DbSet<ProcessStatuDictionary> ProcessStatuDictionary { get; set; }
        public virtual DbSet<QuestionsInfo> QuestionsInfo { get; set; }
        public virtual DbSet<RefundRecord> RefundRecord { get; set; }
        public virtual DbSet<TakeRecord> TakeRecord { get; set; }
        public virtual DbSet<UserBindInfo> UserBindInfo { get; set; }
        public virtual DbSet<PaperQuestionsRelation> PaperQuestionsRelation { get; set; }
        public virtual DbSet<PaperCheckRecord> PaperCheckRecord { get; set; }
        public virtual DbSet<PayTypeDictionary> PayTypeDictionary { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }
}
