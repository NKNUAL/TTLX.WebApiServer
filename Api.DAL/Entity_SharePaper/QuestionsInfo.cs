namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionsInfo")]
    public partial class QuestionsInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5000)]
        public string No { get; set; }

        [StringLength(5000)]
        public string QueContent { get; set; }

        public int? QueType { get; set; }

        [StringLength(4000)]
        public string Option0 { get; set; }

        [StringLength(4000)]
        public string Option1 { get; set; }

        [StringLength(4000)]
        public string Option2 { get; set; }

        [StringLength(4000)]
        public string Option3 { get; set; }

        [StringLength(4000)]
        public string Option4 { get; set; }

        [StringLength(4000)]
        public string Option5 { get; set; }

        [StringLength(4000)]
        public string ResolutionTips { get; set; }

        public int? DifficultLevel { get; set; }

        [StringLength(200)]
        public string SpecialtyId { get; set; }

        [StringLength(5000)]
        public string StandardAnwser { get; set; }

        [Column(TypeName = "image")]
        public byte[] ContentImg { get; set; }

        [Column(TypeName = "image")]
        public byte[] Option0Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] Option1Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] Option2Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] Option3Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] Option4Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] Option5Img { get; set; }

        public int IsDelete { get; set; }

        public string QueVersion { get; set; }
    }
}
