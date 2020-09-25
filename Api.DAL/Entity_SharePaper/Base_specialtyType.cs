namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_specialtyType
    {
        [Key]
        [StringLength(4)]
        public string No { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(5)]
        public string FK_Province { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        [StringLength(50)]
        public string DanxuanScore { get; set; }

        [StringLength(50)]
        public string DuoxuanScore { get; set; }

        [StringLength(50)]
        public string PanduanScore { get; set; }

        [StringLength(50)]
        public string SpecialtyCode { get; set; }

        [StringLength(50)]
        public string ExamDuration { get; set; }
    }
}
