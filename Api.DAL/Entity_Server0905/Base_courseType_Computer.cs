namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_courseType_Computer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int No { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(5)]
        public string FK_Province { get; set; }

        [StringLength(4)]
        public string FK_SpecialtyType { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }
    }
}
