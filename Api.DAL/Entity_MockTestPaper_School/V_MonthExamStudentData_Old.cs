namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamStudentData_Old
    {
        [StringLength(100)]
        public string ExamDate { get; set; }

        public int? PlanID { get; set; }

        [StringLength(100)]
        public string PlanName { get; set; }

        public int? ExamPaperID { get; set; }

        [StringLength(100)]
        public string ExamPaperName { get; set; }

        public double? StudentScore { get; set; }

        [Key]
        [StringLength(100)]
        public string Lexueid { get; set; }

        [StringLength(100)]
        public string StartTime { get; set; }
    }
}
