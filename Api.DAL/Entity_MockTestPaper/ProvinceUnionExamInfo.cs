namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProvinceUnionExamInfo")]
    public partial class ProvinceUnionExamInfo
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string UnionExamNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string StartTime { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string EndTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string NoticeTime { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string WebUrl { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string Remark { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamPaperID { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsInvalid { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(500)]
        public string Contact { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_SpecialtyType { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamTime { get; set; }
    }
}
