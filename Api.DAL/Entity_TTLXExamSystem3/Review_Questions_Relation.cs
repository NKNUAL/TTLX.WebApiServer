namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review_Questions_Relation
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SettleNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string QueNo { get; set; }

        [Key]
        [Column(Order = 3)]
        public double Price { get; set; }
    }
}
