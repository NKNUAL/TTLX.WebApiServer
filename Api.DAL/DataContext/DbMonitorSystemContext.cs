namespace Api.DAL.DataContext
{
    using Api.Core;
    using Api.DAL.Entity_MonitorSystem;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public partial class DbMonitorSystemContext : DbContext
    {
        public DbMonitorSystemContext()
            : base()
        {
            Database.Connection.ConnectionString = ConfigTools.GetDBConnString("dbConnMonitor");
            Database.CommandTimeout = 100;
        }

        public virtual DbSet<Base_Province> Base_Province { get; set; }
        public virtual DbSet<Base_specialtyType> Base_specialtyType { get; set; }
        public virtual DbSet<LogDetails> LogDetails { get; set; }
        public virtual DbSet<PaperInfo> PaperInfo { get; set; }
        public virtual DbSet<Questionsinfo_Local> Questionsinfo_Local { get; set; }
        public virtual DbSet<Questionsinfo_Local2> Questionsinfo_Local2 { get; set; }
        public virtual DbSet<SchoolBasicInfos> SchoolBasicInfos { get; set; }
        public virtual DbSet<SchoolDataUploadTime> SchoolDataUploadTime { get; set; }
        public virtual DbSet<SchoolSpecialtyExpireDate> SchoolSpecialtyExpireDate { get; set; }
        public virtual DbSet<SysDbType> SysDbType { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<UseStatusInfo_Exam> UseStatusInfo_Exam { get; set; }
        public virtual DbSet<UseStatusInfo_Exercise> UseStatusInfo_Exercise { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
