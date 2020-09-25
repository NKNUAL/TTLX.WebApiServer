namespace Api.DAL.Entity_Server0905
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
        public string LocalLexueid { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string SchoolNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string Zhifubao { get; set; }
    }
}
