namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimuDetailScorePoint")]
    public partial class TimuDetailScorePoint
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(20)]
        public string ScorePointID { get; set; }

        [StringLength(20)]
        public string TimuDetailID { get; set; }

        public double? Score { get; set; }

        public int? ResultValueID { get; set; }

        [StringLength(50)]
        public string StartMark { get; set; }

        [StringLength(50)]
        public string EndMark { get; set; }

        public string DfdYqz { get; set; }

        public int? ExamTypeType { get; set; }

        public int? ExamTypeIndex { get; set; }

        public int ExamSmallIndex { get; set; }

        public string ExamStartValue { get; set; }

        public string TimuDesc { get; set; }

        public int? encrypt { get; set; }

        [StringLength(50)]
        public string No { get; set; }
    }
}
