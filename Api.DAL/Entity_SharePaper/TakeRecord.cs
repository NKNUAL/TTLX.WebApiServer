namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TakeRecord")]
    public partial class TakeRecord
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string TakeNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string TakeDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public double TakePrice { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcessStatu { get; set; }
    }
}
