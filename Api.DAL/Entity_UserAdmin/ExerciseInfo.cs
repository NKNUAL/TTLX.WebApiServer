namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExerciseInfo")]
    public partial class ExerciseInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string No { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
        public string FK_CourseType { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(200)]
        public string CourseName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(4)]
        public string FK_SpecialtyType { get; set; }

        public int? CompleteCount { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UseStatus { get; set; }

        [StringLength(500)]
        public string ClassDesc { get; set; }
    }
}
