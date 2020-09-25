namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScoreResultDetail")]
    public partial class ScoreResultDetail
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string LexueID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string StuName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string StuAnswer { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string StandAnswer { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string QueNo { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string QueType { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string QueScore { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string KsjhID { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string ExamPaperID { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string SubmitTime { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(100)]
        public string GetScore { get; set; }
    }
}
