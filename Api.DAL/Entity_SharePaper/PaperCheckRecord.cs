namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaperCheckRecord")]
    public partial class PaperCheckRecord
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string PaperID { get; set; }


        public int CheckStatu { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [StringLength(100)]
        public string CheckDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string CheckUserId { get; set; }
    }
}
