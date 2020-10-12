namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MockTestPaperQuestionRelation_Computer
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string PaperID { get; set; }

        [StringLength(50)]
        public string QuestionID { get; set; }

        public int? Point { get; set; }

        public int? QuestionType { get; set; }

        public int? OrderIndex { get; set; }
    }
}
