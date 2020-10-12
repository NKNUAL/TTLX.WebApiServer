namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_CultureExamPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CulturePlanId { get; set; }

        public int? PlanId { get; set; }

        public int? SpecialtyId { get; set; }

        [StringLength(20)]
        public string SpecialtyName { get; set; }

        [StringLength(100)]
        public string PlanName { get; set; }
    }
}
