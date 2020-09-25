namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SXTTable")]
    public partial class SXTTable
    {
        [Key]
        [StringLength(50)]
        public string STimuID { get; set; }

        public string TimuDesc { get; set; }

        public string Kemu { get; set; }

        [StringLength(50)]
        public string CreateTime { get; set; }

        public int? sTimuScore { get; set; }

        [StringLength(50)]
        public string NO { get; set; }
    }
}
