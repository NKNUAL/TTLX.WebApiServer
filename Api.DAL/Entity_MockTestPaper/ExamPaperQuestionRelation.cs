namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamPaperQuestionRelation")]
    public partial class ExamPaperQuestionRelation
    {
        public int ID { get; set; }

        public int? PaperID { get; set; }

        [StringLength(50)]
        public string QuestionID { get; set; }

        public int? Point { get; set; }

        public int? QuestionType { get; set; }
    }
}
