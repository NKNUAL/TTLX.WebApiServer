namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ErrorQuestionReviewUser")]
    public partial class ErrorQuestionReviewUser
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string QuestionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ReviewUserId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string ReviewUserName { get; set; }
    }
}
