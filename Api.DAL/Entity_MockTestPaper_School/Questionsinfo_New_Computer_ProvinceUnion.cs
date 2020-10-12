namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionsinfo_New_Computer_ProvinceUnion
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string No { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        public int? Type { get; set; }

        [StringLength(10)]
        public string ParentNo { get; set; }

        [StringLength(4000)]
        public string Question { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Point { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public int? TestClick { get; set; }

        public int? QuestionLevel { get; set; }

        public int? ReferenceCount { get; set; }

        public int? IsShared { get; set; }

        [StringLength(200)]
        public string FK_Province { get; set; }

        [StringLength(200)]
        public string FK_SpecialtyType { get; set; }

        [StringLength(200)]
        public string FK_CourseType { get; set; }

        [StringLength(200)]
        public string FK_Chapter { get; set; }

        [StringLength(200)]
        public string FK_KnowledgePoint { get; set; }

        [StringLength(50)]
        public string FK_Creater { get; set; }

        [StringLength(50)]
        public string FK_Crew { get; set; }

        public int? IsFather { get; set; }

        [StringLength(30)]
        public string UpdateTime { get; set; }

        [StringLength(4000)]
        public string StandardAnwserText { get; set; }

        public int? StarLevel { get; set; }

        [StringLength(100)]
        public string Option0Text { get; set; }

        [StringLength(4000)]
        public string StandardAnwser { get; set; }

        [StringLength(10)]
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

        [Column(TypeName = "image")]
        public byte[] questionImg { get; set; }

        public int? IsPicAndTextVersion { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic1 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic2 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic3 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic4 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic5 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic6 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic7 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic8 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic9 { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic10 { get; set; }

        [StringLength(200)]
        public string pic1Name { get; set; }

        [StringLength(200)]
        public string pic2Name { get; set; }

        [StringLength(200)]
        public string pic3Name { get; set; }

        [StringLength(200)]
        public string pic4Name { get; set; }

        [StringLength(200)]
        public string pic5Name { get; set; }

        [StringLength(200)]
        public string pic6Name { get; set; }

        [StringLength(200)]
        public string pic7Name { get; set; }

        [StringLength(200)]
        public string pic8Name { get; set; }

        [StringLength(200)]
        public string pic9Name { get; set; }

        [StringLength(200)]
        public string pic10Name { get; set; }

        [StringLength(50)]
        public string sourcedoc { get; set; }
    }
}
