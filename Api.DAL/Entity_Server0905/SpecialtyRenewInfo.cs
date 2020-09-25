namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialtyRenewInfo")]
    public partial class SpecialtyRenewInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string SpecialtyName { get; set; }

        [Key]
        [Column(Order = 3)]
        public double RenewYear { get; set; }

        [Key]
        [Column(Order = 4)]
        public double RenewPrice { get; set; }
    }
}
