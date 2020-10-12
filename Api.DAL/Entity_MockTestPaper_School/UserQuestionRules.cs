namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserQuestionRules
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string RuleNo { get; set; }

        public string RuleName { get; set; }

        public string RuleDesc { get; set; }

        public string UserId { get; set; }

        public string SpecialtyId { get; set; }
        public int DanxuanCount { get; set; }
        public int DuoxuanCount { get; set; }
        public int PanduanCount { get; set; }
        public int TotalCount { get; set; }
        public int IsDelete { get; set; }
    }
}
