namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaperInfo")]
    public partial class PaperInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string PaperID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string PaperName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string PaperCreateDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string PaperUserId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string PaperSpecialtyId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1000)]
        public string PaperDesc { get; set; }

        [Key]
        [Column(Order = 7)]
        public double PaperPrice { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaperQueCount { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DanxuanNum { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DuoxuanNum { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PanduanNum { get; set; }

        public int CheckStatu { get; set; }

        public int PaperStatu { get; set; }

        public string PaperVersion { get; set; }
    }
}
