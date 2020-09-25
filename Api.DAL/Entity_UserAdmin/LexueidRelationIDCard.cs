namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LexueidRelationIDCard")]
    public partial class LexueidRelationIDCard
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string lexueid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string idcard { get; set; }

        [StringLength(50)]
        public string phoneNumber { get; set; }

        [StringLength(50)]
        public string qqNumber { get; set; }

        [StringLength(100)]
        public string MachineCode { get; set; }
    }
}
