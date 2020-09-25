namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocalRegInfo")]
    public partial class LocalRegInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SpecialtyCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string SpecialtyName { get; set; }

        [StringLength(100)]
        public string BankVersion { get; set; }

        [StringLength(100)]
        public string LastUpdateTime { get; set; }
    }
}
