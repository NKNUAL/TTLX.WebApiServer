namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTable")]
    public partial class UserTable
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Lexueid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string UserPassword { get; set; }

        [StringLength(100)]
        public string IDcard { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UseState { get; set; }

        [StringLength(100)]
        public string FK_School { get; set; }

        [StringLength(100)]
        public string FK_SchoolID { get; set; }

        [StringLength(5)]
        public string FK_Province { get; set; }

        [StringLength(100)]
        public string UserDesc { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string QQ { get; set; }
    }
}
