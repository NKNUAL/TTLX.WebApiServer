namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionsDeductRecord")]
    public partial class QuestionsDeductRecord
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string SettleNo { get; set; }

        [StringLength(100)]
        public string Lexueid { get; set; }

        [StringLength(100)]
        public string QueNo { get; set; }

        public double? DeductAmount { get; set; }

        [StringLength(200)]
        public string DeductReason { get; set; }
    }
}
