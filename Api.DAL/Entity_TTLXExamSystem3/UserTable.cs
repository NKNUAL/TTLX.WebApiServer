namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTable")]
    public partial class UserTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [StringLength(100)]
        public string lexueid { get; set; }

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
        public string FK_School { get; set; }

        public int? FK_SchoolID { get; set; }

        [StringLength(100)]
        public string UserDesc { get; set; }

        public int? UserType { get; set; }

        [StringLength(100)]
        public string UserClass { get; set; }

        public int? isAdmin { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string nameDesc { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? UserID { get; set; }
    }
}
