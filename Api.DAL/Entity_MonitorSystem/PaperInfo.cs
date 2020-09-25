namespace Api.DAL.Entity_MonitorSystem
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
        [StringLength(100)]
        public string PaperId { get; set; }

        [StringLength(50)]
        public string PaperName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaperType { get; set; }

        public int? TotalPeopleNum { get; set; }

        public int? PeopleNum { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string PaperCreateTime { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string UseStatusId { get; set; }
    }
}
