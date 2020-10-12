namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MonthExamTestPaperScoreResult")]
    public partial class MonthExamTestPaperScoreResult
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string paperid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string lexueid { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int specialtyid { get; set; }

        [Key]
        [Column(Order = 4)]
        public double studentScore { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string submitTime { get; set; }
    }
}
