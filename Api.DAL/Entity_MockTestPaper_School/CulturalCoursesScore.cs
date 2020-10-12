namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CulturalCoursesScore")]
    public partial class CulturalCoursesScore
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string IDCard { get; set; }

        [StringLength(50)]
        public string Lexueid { get; set; }

        [StringLength(20)]
        public string StudentName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamPlanId { get; set; }

        public double? ChineseScore { get; set; }

        public double? MathScore { get; set; }

        public double? EnglishScore { get; set; }

        public double? StudentScore { get; set; }

        public string AnswerDetail { get; set; }
    }
}
