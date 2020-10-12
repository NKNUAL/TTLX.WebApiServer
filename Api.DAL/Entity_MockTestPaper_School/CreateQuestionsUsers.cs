namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CreateQuestionsUsers
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Lexueid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(4000)]
        public string SchoolName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_Specialty { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string FK_SpecialtyName { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string UserPwd { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string Zhifubao { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserType { get; set; }

        public int? UseState { get; set; }

        [StringLength(100)]
        public string CourseModule { get; set; }
    }
}
