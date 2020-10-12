namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamCompare_Computer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string KsjhName { get; set; }

        public double? PaperQueTotalScore { get; set; }

        public double? StudentScore { get; set; }

        [StringLength(100)]
        public string FK_School { get; set; }

        [StringLength(100)]
        public string FK_SchoolID { get; set; }
    }
}
