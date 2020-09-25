namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderRecord")]
    public partial class OrderRecord
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string OrderNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string CreateUserId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string PaperID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string OrderDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string ExpireDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public double OrderPrice { get; set; }

        public int OrderStatu { get; set; }


        public int PayType { get; set; }
    }
}
