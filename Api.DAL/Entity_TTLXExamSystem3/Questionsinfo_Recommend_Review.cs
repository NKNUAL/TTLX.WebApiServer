namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionsinfo_Recommend_Review
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
        public double SettleMoney { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string SettleTime { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string ReviewUserName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string ReviewUserPhone { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string ReviewUserZhifubao { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsProcess { get; set; }

        [StringLength(100)]
        public string PorcessTime { get; set; }
    }
}
