namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysMenu")]
    public partial class SysMenu
    {
        [Key]
        public int MID { get; set; }

        [StringLength(100)]
        public string MNAME { get; set; }

        [StringLength(200)]
        public string MIMG { get; set; }

        [StringLength(400)]
        public string MURL { get; set; }

        public int? MPID { get; set; }
    }
}
