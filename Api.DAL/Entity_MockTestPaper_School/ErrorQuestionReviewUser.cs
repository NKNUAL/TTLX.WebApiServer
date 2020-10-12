namespace Api.DAL.Entity_MockTestPaper_School
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
        [Column(Order = 0)]
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
