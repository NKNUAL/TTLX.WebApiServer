namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RenewRecordRelation")]
    public partial class RenewRecordRelation
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string RenewNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 3)]
        public double RenewPrice { get; set; }

        [Key]
        [Column(Order = 4)]
        public double RenewYears { get; set; }
    }
}
