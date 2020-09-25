namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserQuestionMockTestPaperQuestionRelation")]
    public partial class UserQuestionMockTestPaperQuestionRelation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        public string PaperID { get; set; }

        [StringLength(50)]
        public string QuestionID { get; set; }

        public int? QuestionType { get; set; }

        public int? OrderIndex { get; set; }
    }
}
