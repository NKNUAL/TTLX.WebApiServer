namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTable")]
    public partial class UserTable
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string lexueid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string KaoHao { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string UserPassword { get; set; }

        [StringLength(100)]
        public string IDcard { get; set; }

        public int? UseState { get; set; }

        public int? FK_Specialty { get; set; }

        [StringLength(100)]
        public string FK_SpecialtyName { get; set; }

        [StringLength(100)]
        public string UnionExamSpecialtyCode { get; set; }

        [StringLength(100)]
        public string FK_School { get; set; }

        [StringLength(100)]
        public string FK_SchoolID { get; set; }

        [StringLength(100)]
        public string UserDesc { get; set; }

        public int? UserType { get; set; }

        [StringLength(100)]
        public string UserClass { get; set; }

        [StringLength(100)]
        public string UserClassCode { get; set; }

        public int? IsAdmin { get; set; }

        public int? IsLocked { get; set; }

        public int? IsDelete { get; set; }
    }
}
