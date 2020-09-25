namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamType")]
    public partial class ExamType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("ExamType")]
        public int? ExamType1 { get; set; }

        [StringLength(50)]
        public string ExamTypeName { get; set; }

        public int? ExamTypeParent { get; set; }

        public int? HaveFunction { get; set; }

        public int? IsResultOrProcess { get; set; }

        [StringLength(250)]
        public string DfdDesc { get; set; }

        public bool? isConfig { get; set; }

        public bool? isResult { get; set; }

        public bool? isPoint { get; set; }

        [StringLength(10)]
        public string FK_SpecialtyType { get; set; }

        public bool? State { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public bool? Mode { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}
