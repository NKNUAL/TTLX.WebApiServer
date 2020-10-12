namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamPeopleCount_Old
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }

        public int? PeopleCount { get; set; }
    }
}
