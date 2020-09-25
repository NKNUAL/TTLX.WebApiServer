namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ErrorQuestions
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SpecialtyCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string QuestionId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(4000)]
        public string ErrorDesc { get; set; }

        [StringLength(4000)]
        public string ErrorTag { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string SubmitUserId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string SubmitDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string SchoolCode { get; set; }

        public int? IsCorrect { get; set; }

        public int? DbType { get; set; }
    }
}
