namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LocalPaperQuestions
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string QGuid { get; set; }


        public string CourseNo { get; set; }


        public string KnowNo { get; set; }

        public string QueContent { get; set; }


        public int QueType { get; set; }


        public int DifficultLevel { get; set; }

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

        [StringLength(100)]
        public string Answer { get; set; }

        [Column(TypeName = "image")]
        public byte[] NameImg { get; set; }

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
    }
}
