namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserQuestionRules_Course_Relation
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Column(Order = 1)]
        [StringLength(50)]
        public string RuleNo { get; set; }


        [Column(Order = 2)]
        [StringLength(50)]
        public string CourseRuleNo { get; set; }


        [Column(Order = 3)]
        [StringLength(4)]
        public string CourseNo { get; set; }



        public int TotalCount { get; set; }
        public int DanxuanCount { get; set; }
        public int DuoxuanCount { get; set; }
        public int PanduanCount { get; set; }
    }
}
