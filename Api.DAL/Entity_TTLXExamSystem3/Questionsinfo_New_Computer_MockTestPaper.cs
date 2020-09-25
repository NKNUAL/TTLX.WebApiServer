namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionsinfo_New_Computer_MockTestPaper
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5000)]
        public string No { get; set; }

        [StringLength(5000)]
        public string Name { get; set; }

        public int? Type { get; set; }

        [StringLength(5000)]
        public string Question { get; set; }

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
        public string Option6 { get; set; }

        [StringLength(4000)]
        public string Option7 { get; set; }

        [StringLength(4000)]
        public string Option8 { get; set; }

        [StringLength(4000)]
        public string Option9 { get; set; }

        [StringLength(4000)]
        public string ResolutionTips { get; set; }

        public int? DifficultLevel { get; set; }

        public int? UseCount { get; set; }

        [StringLength(200)]
        public string FK_SpecialtyType { get; set; }

        [StringLength(200)]
        public string FK_CourseType { get; set; }

        [StringLength(200)]
        public string FK_KnowledgePoint { get; set; }

        [StringLength(5000)]
        public string StandardAnwser { get; set; }

        [StringLength(5000)]
        public string StandardMultiAnswer { get; set; }

        [Column(TypeName = "image")]
        public byte[] nameImg { get; set; }

        [Column(TypeName = "image")]
        public byte[] option0Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] option1Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] option2Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] option3Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] option4Img { get; set; }

        [Column(TypeName = "image")]
        public byte[] option5Img { get; set; }

        [StringLength(500)]
        public string sourcedoc { get; set; }

        [StringLength(100)]
        public string CreateUserPhoneNumber { get; set; }

        [StringLength(100)]
        public string CreateUserName { get; set; }

        [StringLength(100)]
        public string CreateTime { get; set; }

        public int? IsDelete { get; set; }

        [StringLength(100)]
        public string VersionFlag { get; set; }
    }
}
