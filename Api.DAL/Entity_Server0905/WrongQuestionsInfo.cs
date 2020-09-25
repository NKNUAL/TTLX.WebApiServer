namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WrongQuestionsInfo")]
    public partial class WrongQuestionsInfo
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string lexueid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string QuestionID { get; set; }

        [StringLength(500)]
        public string StudentAnswer { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentScore { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionScore { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string AnswerTime { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionOrigin { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OriginPaperId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(200)]
        public string OriginPaperName { get; set; }
    }
}
