namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimuDetail")]
    public partial class TimuDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("TimuDetail")]
        [StringLength(550)]
        public string TimuDetail1 { get; set; }

        [StringLength(20)]
        public string TimuID { get; set; }

        public string TimuDesc { get; set; }

        public int? TimuScore { get; set; }

        [StringLength(50)]
        public string TimuFileName { get; set; }

        [StringLength(50)]
        public string TimuType { get; set; }

        [StringLength(10)]
        public string TimuSteps { get; set; }

        [Column(TypeName = "image")]
        public byte[] TimuFile { get; set; }

        [Column(TypeName = "image")]
        public byte[] TimuSC1 { get; set; }

        [Column(TypeName = "image")]
        public byte[] TimuSC2 { get; set; }

        [Column(TypeName = "image")]
        public byte[] TimuSC3 { get; set; }

        [StringLength(10)]
        public string Filehouzhui { get; set; }

        [StringLength(10)]
        public string SC1houzui { get; set; }

        [StringLength(10)]
        public string SC2houzui { get; set; }

        [StringLength(10)]
        public string SC3houzui { get; set; }

        [StringLength(10)]
        public string TimuLevel { get; set; }

        [StringLength(10)]
        public string FK_SpecialtyType { get; set; }

        public int? encrypt { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        public int? SubType { get; set; }
    }
}
