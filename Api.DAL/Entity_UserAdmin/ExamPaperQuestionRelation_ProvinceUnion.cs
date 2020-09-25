namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExamPaperQuestionRelation_ProvinceUnion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? PaperID { get; set; }

        [StringLength(50)]
        public string QuestionID { get; set; }

        public double? Point { get; set; }

        public int? QuestionType { get; set; }
    }
}
