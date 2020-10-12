namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_CultureExamStudentScore
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string IDCard { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Lexueid { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        public double? ChineseScore { get; set; }

        public double? EnglishScore { get; set; }

        public double? MathScore { get; set; }

        public double? CultureStudentScore { get; set; }

        [StringLength(100)]
        public string SchoolCode { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }

        [StringLength(100)]
        public string SpecialtyName { get; set; }
    }
}
