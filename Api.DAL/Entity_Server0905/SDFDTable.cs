namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SDFDTable")]
    public partial class SDFDTable
    {
        [StringLength(50)]
        public string ID { get; set; }

        public int? ScorePointID { get; set; }

        public double? Score { get; set; }

        [StringLength(50)]
        public string ResultValueID { get; set; }

        [StringLength(50)]
        public string StartMark { get; set; }

        [StringLength(50)]
        public string EndMark { get; set; }

        public string DfdYqz { get; set; }

        public int? ExamTypeType { get; set; }

        public int? ExamTypeIndex { get; set; }

        public string ExamStartValue { get; set; }

        public string TimuDesc { get; set; }

        [StringLength(50)]
        public string CreateTime { get; set; }

        [StringLength(50)]
        public string kemuName { get; set; }

        [StringLength(20)]
        public string No { get; set; }
    }
}
