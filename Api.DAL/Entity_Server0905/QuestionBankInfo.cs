namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionBankInfo")]
    public partial class QuestionBankInfo
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

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string BankVersion { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }
    }
}
