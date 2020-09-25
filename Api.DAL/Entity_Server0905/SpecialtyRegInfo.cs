namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialtyRegInfo")]
    public partial class SpecialtyRegInfo
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

        public int UseStatus { get; set; }

        public string ExpireTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string SchoolCode { get; set; }
    }
}
