namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Settlement_Questions_AddCut
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Lexueid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string SettleNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string QueNo { get; set; }

        [Key]
        [Column(Order = 4)]
        public double Price { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }
    }
}
