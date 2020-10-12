namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_StudentScoreDetail_Old
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }

        [StringLength(100)]
        public string Lexueid { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        public double? StudentScore { get; set; }

        [StringLength(100)]
        public string SchoolCode { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }

        [StringLength(100)]
        public string SpecialtyName { get; set; }

        public int? AnswerTime { get; set; }

        public string AnswerDetail { get; set; }
    }
}
