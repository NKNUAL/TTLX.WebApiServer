namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResultValueTable")]
    public partial class ResultValueTable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ResultDetail { get; set; }

        [StringLength(50)]
        public string ResultDesc { get; set; }

        public int? TimuID { get; set; }

        public int? CodeValue { get; set; }
    }
}
