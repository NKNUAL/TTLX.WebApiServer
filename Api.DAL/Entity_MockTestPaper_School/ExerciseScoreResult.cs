namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExerciseScoreResult")]
    public partial class ExerciseScoreResult
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ExerciseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string PaperId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string LexueId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 5)]
        public double GetScore { get; set; }

        [StringLength(50)]
        public string SubmitTime { get; set; }
    }
}
