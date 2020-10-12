namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExercisePaperRelation")]
    public partial class ExercisePaperRelation
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ExerciseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string PaperID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string PaperName { get; set; }

        public int? IsUpload { get; set; }
    }
}
