namespace Api.DAL.Entity_MockTestPaper
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

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RuleNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string RuleName { get; set; }

        [StringLength(1000)]
        public string RuleDesc { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(4)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueCount { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsDelete { get; set; }
    }
}
