namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_School
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SchoolNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string SchoolName { get; set; }

        [StringLength(100)]
        public string FK_Province { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [StringLength(500)]
        public string GPCode { get; set; }
    }
}
