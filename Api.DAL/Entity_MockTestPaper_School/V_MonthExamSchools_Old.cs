namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamSchools_Old
    {
        [StringLength(100)]
        public string SchoolNo { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }

        [StringLength(50)]
        public string AreaNo { get; set; }

        [StringLength(50)]
        public string AreaName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }
    }
}
