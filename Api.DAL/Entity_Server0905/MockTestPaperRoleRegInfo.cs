namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MockTestPaperRoleRegInfo")]
    public partial class MockTestPaperRoleRegInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsMockTestPaper { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsMonthExamPaper { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string ExpireTime { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string SchoolCode { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string MockTestPaperVersion { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string MonthExamPaperVersion { get; set; }
    }
}
