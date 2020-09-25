namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckStatuDictionary")]
    public partial class CheckStatuDictionary
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CheckStatuName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CheckStatu { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
