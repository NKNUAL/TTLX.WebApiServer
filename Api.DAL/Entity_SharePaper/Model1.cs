namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<PaperCheckRecord> PaperCheckRecord { get; set; }
        public virtual DbSet<PayTypeDictionary> PayTypeDictionary { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaperCheckRecord>()
                .Property(e => e.PaperID)
                .IsUnicode(false);

            modelBuilder.Entity<PaperCheckRecord>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<PaperCheckRecord>()
                .Property(e => e.CheckDate)
                .IsUnicode(false);

            modelBuilder.Entity<PaperCheckRecord>()
                .Property(e => e.CheckUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PayTypeDictionary>()
                .Property(e => e.PayTypeName)
                .IsUnicode(false);
        }
    }
}
