namespace Api.DAL.Entity_Server0905
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
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GetScore { get; set; }
    }
}
