namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamPlan_Old
    {
        public int? PlanId { get; set; }

        [StringLength(100)]
        public string PlanName { get; set; }

        [StringLength(100)]
        public string SchoolNo { get; set; }

        public int? SpecialtyId { get; set; }

        [Key]
        [StringLength(100)]
        public string StudentId { get; set; }
    }
}
