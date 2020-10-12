namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserQuestionRules_Know_Relation
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string CourseRuleNo { get; set; }
        public string KnowNo { get; set; }
        public int? DanxuanCount { get; set; }
        public int? DuoxuanCount { get; set; }
        public int? PanduanCount { get; set; }
    }
}
