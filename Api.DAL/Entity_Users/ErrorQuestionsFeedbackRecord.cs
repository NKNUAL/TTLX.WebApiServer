namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ErrorQuestionsFeedbackRecord")]
    public partial class ErrorQuestionsFeedbackRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string QuestionId { get; set; }

        [StringLength(4000)]
        public string ErrorTag { get; set; }

        [StringLength(4000)]
        public string ErrorDesc { get; set; }

        [StringLength(100)]
        public string SubmitUserId { get; set; }

        [StringLength(100)]
        public string SubmitDate { get; set; }

        [StringLength(100)]
        public string SchoolCode { get; set; }
    }
}
