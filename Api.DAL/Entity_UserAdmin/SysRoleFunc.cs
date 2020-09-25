namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysRoleFunc")]
    public partial class SysRoleFunc
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string FNAME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string FUNCDESC { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FPID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Platform { get; set; }
    }
}
