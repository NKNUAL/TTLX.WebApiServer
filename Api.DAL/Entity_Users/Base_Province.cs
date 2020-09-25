namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_Province
    {
        [Key]
        [StringLength(5)]
        public string No { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(30)]
        public string TotalName { get; set; }
    }
}
