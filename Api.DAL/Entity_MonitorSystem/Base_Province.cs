namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_Province
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string No { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string TotalName { get; set; }
    }
}
