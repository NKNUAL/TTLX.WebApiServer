namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_TotalErrorQuestion
    {
        public Guid? Guid { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string SpecialtyType { get; set; }

        public int? QueType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string QueNo { get; set; }

        public int? IsCorrect { get; set; }

        public int? DbType { get; set; }

        [StringLength(100)]
        public string ReviewUserId { get; set; }

        [StringLength(100)]
        public string ReviewUserName { get; set; }

        public int? FeedbackCount { get; set; }

        public int? ModifyCount { get; set; }

        public string QueName { get; set; }

        [StringLength(4000)]
        public string OptionA { get; set; }

        [StringLength(4000)]
        public string OptionB { get; set; }

        [StringLength(4000)]
        public string OptionC { get; set; }

        [StringLength(4000)]
        public string OptionD { get; set; }

        [StringLength(200)]
        public string CourseType { get; set; }

        [StringLength(5000)]
        public string QueAnswer { get; set; }

        [StringLength(200)]
        public string QueSource { get; set; }

        [StringLength(200)]
        public string DbTypeName { get; set; }

        [StringLength(4000)]
        public string ResolutionTips { get; set; }
    }
}
