namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SXTDFDRelation")]
    public partial class SXTDFDRelation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        public string STimuID { get; set; }

        [StringLength(50)]
        public string DFDID { get; set; }

        [StringLength(10)]
        public string DNo { get; set; }
    }
}
