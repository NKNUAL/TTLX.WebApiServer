namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CulturalExamPlan")]
    public partial class CulturalExamPlan
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string CultureExamName { get; set; }

        public int? SpecialtyId { get; set; }

        [StringLength(20)]
        public string SpecialtyName { get; set; }

        [StringLength(20)]
        public string ExamDate { get; set; }
    }
}
