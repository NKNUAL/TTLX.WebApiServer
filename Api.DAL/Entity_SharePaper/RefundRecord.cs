namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RefundRecord")]
    public partial class RefundRecord
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string RefundNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string OrderNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string RefundDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public double RefundPrice { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcessStatu { get; set; }
    }
}
