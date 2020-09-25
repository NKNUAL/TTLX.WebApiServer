namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExamPaper_Computer
    {
        [Key]
        [Column(Order = 0)]
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

        public int? bianchengNum { get; set; }

        public int? win7Num { get; set; }

        public int? wangluoNum { get; set; }

        public int? wordNum { get; set; }

        public int? excelNum { get; set; }

        public int? pptNum { get; set; }

        public int? accessnum { get; set; }

        public int? danxuanScore { get; set; }

        public int? duoxuanScore { get; set; }

        public int? panduanScore { get; set; }

        public int? bianchengScore { get; set; }

        public int? win7Score { get; set; }

        public int? wangluoScore { get; set; }

        public int? wordScore { get; set; }

        public int? excelScore { get; set; }

        public int? pptScore { get; set; }

        public int? accessscore { get; set; }

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
