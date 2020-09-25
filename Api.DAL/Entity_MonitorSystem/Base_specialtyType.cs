namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_specialtyType
    {
        [StringLength(4)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string FK_Province { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string SpecialtyCode { get; set; }
    }
}
