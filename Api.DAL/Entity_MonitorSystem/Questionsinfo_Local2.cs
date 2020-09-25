namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questionsinfo_Local2
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string QueNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5000)]
        public string QueContent { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueType { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string OptionA { get; set; }

        [StringLength(100)]
        public string OptionB { get; set; }

        [StringLength(100)]
        public string OptionC { get; set; }

        [StringLength(100)]
        public string OptionD { get; set; }

        [StringLength(100)]
        public string OptionE { get; set; }

        [StringLength(100)]
        public string OptionF { get; set; }

        [StringLength(1000)]
        public string ResolutionTips { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UseCount { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(200)]
        public string FK_SpecialtyType { get; set; }

        [StringLength(200)]
        public string FK_CourseType { get; set; }

        [StringLength(200)]
        public string FK_CourseTypeName { get; set; }

        [StringLength(200)]
        public string FK_KnowledgePoint { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string StandardAnwser { get; set; }

        [Column(TypeName = "image")]
        public byte[] nameImg { get; set; }

        [Column(TypeName = "image")]
        public byte[] optionAImg { get; set; }

        [Column(TypeName = "image")]
        public byte[] optionBImg { get; set; }

        [Column(TypeName = "image")]
        public byte[] optionCImg { get; set; }

        [Column(TypeName = "image")]
        public byte[] optionDImg { get; set; }

        [StringLength(500)]
        public string sourcedoc { get; set; }

        [StringLength(100)]
        public string CreateUserId { get; set; }

        [StringLength(100)]
        public string CreateUserName { get; set; }

        [StringLength(100)]
        public string CreateTime { get; set; }

        public int? IsDelete { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string SchoolCode { get; set; }
    }
}
