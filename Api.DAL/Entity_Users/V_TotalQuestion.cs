namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_TotalQuestion
    {
        public Guid? Guid { get; set; }

        [StringLength(5000)]
        public string QueNo { get; set; }

        public string QueName { get; set; }

        public int? QueType { get; set; }

        [StringLength(4000)]
        public string OptionA { get; set; }

        [StringLength(4000)]
        public string OptionB { get; set; }

        [StringLength(4000)]
        public string OptionC { get; set; }

        [StringLength(4000)]
        public string OptionD { get; set; }

        [StringLength(4000)]
        public string ResolutionTips { get; set; }

        [StringLength(200)]
        public string SpecialtyType { get; set; }

        [StringLength(200)]
        public string CourseType { get; set; }

        [StringLength(5000)]
        public string QueAnswer { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DbType { get; set; }

        [StringLength(200)]
        public string QueSource { get; set; }
    }
}
