namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SXTTimuTable")]
    public partial class SXTTimuTable
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string STimuID { get; set; }

        [StringLength(50)]
        public string TimuID { get; set; }

        [StringLength(10)]
        public string SNo { get; set; }
    }
}
