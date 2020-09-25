namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProvinceUnionExamStudentList")]
    public partial class ProvinceUnionExamStudentList
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
        public string UnionExamNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string StudentName { get; set; }

        [StringLength(100)]
        public string StudentPCAddress { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string StudentClass { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string SubmitTime { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DurationTime { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(10)]
        public string Score { get; set; }
    }
}
