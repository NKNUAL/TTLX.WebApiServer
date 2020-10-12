namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionsinfo_Recommend
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string QueNo { get; set; }

        [Key]
        [Column(Order = 2)]
        public string QueContent { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1000)]
        public string OptionA { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1000)]
        public string OptionB { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1000)]
        public string OptionC { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1000)]
        public string OptionD { get; set; }

        [StringLength(1000)]
        public string OptionE { get; set; }

        [StringLength(1000)]
        public string OptionF { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DifficultLevel { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string StandardAnwser { get; set; }

        [StringLength(1000)]
        public string ResolutionTips { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string FK_Specialty { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string FK_Course { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(100)]
        public string FK_KnowledgePoint { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(100)]
        public string KnowledgePointName { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(100)]
        public string CreateUserPhoneNumber { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(100)]
        public string CreateUserName { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(100)]
        public string CreateTime { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueStatus { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueType { get; set; }

        [StringLength(500)]
        public string NoPassReason { get; set; }

        public int? IsDelete { get; set; }
    }
}
