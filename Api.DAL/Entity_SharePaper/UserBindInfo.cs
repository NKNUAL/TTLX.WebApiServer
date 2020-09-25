namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserBindInfo")]
    public partial class UserBindInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string UserToken { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string LocalLexueid { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string SchoolNo { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string ZhifubaoName { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string Zhifubao { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string BindDate { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UseStatu { get; set; }
    }
}
