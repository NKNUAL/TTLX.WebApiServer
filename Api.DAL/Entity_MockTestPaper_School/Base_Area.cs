namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_Area
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string AreaNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string FK_Province { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string AreaName { get; set; }
    }
}
