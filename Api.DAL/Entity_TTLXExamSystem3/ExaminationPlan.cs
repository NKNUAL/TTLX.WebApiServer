namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExaminationPlan")]
    public partial class ExaminationPlan
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string KsjhName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpecialtyId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string SpecialtyName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string StartTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamTime { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string ClassDesc { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamPaperId { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsEnd { get; set; }
    }
}
