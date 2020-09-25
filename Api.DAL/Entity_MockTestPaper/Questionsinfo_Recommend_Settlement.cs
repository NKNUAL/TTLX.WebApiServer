namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionsinfo_Recommend_Settlement
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SettleMoney { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string SettleTime { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string CreateUserName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string CreateUserPhone { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string CreateUserZhifubao { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsProcess { get; set; }

        [StringLength(100)]
        public string PorcessTime { get; set; }
    }
}
