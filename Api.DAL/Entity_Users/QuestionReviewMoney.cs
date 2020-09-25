namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionReviewMoney")]
    public partial class QuestionReviewMoney
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReviewStatus { get; set; }

        [Key]
        [Column(Order = 2)]
        public double ReviewMoney { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
