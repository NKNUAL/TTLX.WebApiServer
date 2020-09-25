namespace Api.DAL.Entity_MockTestPaper
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

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RuleNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string CourseRuleNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
        public string CourseNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueCount { get; set; }
    }
}
