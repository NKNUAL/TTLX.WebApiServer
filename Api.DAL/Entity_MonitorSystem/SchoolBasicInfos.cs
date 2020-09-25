namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SchoolBasicInfos
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string SchoolId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SchoolName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string SchoolCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string SchoolGpCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(5)]
        public string FK_Province { get; set; }
    }
}
