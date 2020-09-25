namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_courseType
    {
        [Key]
        [StringLength(4)]
        public string No { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(5)]
        public string FK_Province { get; set; }

        [StringLength(4)]
        public string FK_SpecialtyType { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
