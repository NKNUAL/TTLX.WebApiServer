namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SaveScoreResult")]
    public partial class SaveScoreResult
    {
        [Key]
        [StringLength(50)]
        public string EmpID { get; set; }

        [StringLength(50)]
        public string EmpName { get; set; }

        [StringLength(50)]
        public string Score { get; set; }

        [StringLength(50)]
        public string time { get; set; }

        [StringLength(10)]
        public string caozuo1Score { get; set; }

        [StringLength(10)]
        public string caozuo2Score { get; set; }

        [StringLength(10)]
        public string caozuo3Score { get; set; }

        [StringLength(10)]
        public string caozuo4Score { get; set; }

        [StringLength(10)]
        public string caozuo5Score { get; set; }

        [StringLength(50)]
        public string KSJH { get; set; }

        public int? bedNo { get; set; }

        [StringLength(50)]
        public string shijuanName { get; set; }

        public string beizhu { get; set; }

        [StringLength(10)]
        public string caozuo6Score { get; set; }
    }
}
