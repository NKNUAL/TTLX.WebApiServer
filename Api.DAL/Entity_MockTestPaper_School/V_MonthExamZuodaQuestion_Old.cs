namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamZuodaQuestion_Old
    {
        [StringLength(100)]
        public string QueNo { get; set; }

        public string QueName { get; set; }

        [StringLength(100)]
        public string CorrectAnswer { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }
    }
}
