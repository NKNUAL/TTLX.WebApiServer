namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MockTestPaperRule")]
    public partial class MockTestPaperRule
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RuleNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string FK_CourseType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string FK_CourseName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string FK_KnowledgePoint { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string FK_KnowledgePointName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(500)]
        public string PaperNo { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TimuType { get; set; }
    }
}
