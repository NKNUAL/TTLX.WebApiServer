namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamPaper")]
    public partial class ExamPaper
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamPaperID { get; set; }

        public int? PaperQueCount { get; set; }

        public int? PaperQueTotalScore { get; set; }

        public int? PaperType { get; set; }

        [StringLength(200)]
        public string ExamPaperName { get; set; }

        [StringLength(100)]
        public string ExamPaperCreateTime { get; set; }

        public int? danxuanNum { get; set; }

        public int? duoxuanNum { get; set; }

        public int? panduanNum { get; set; }

        public int? danxuanScore { get; set; }

        public int? duoxuanScore { get; set; }

        public int? panduanScore { get; set; }

        [StringLength(100)]
        public string CreateUserID { get; set; }

        [StringLength(100)]
        public string CreateUserName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_Specialty { get; set; }

        public int? isDelete { get; set; }
    }
}
