namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PaperCaozuoTimuRelation_Cloud
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string ExamPaperID { get; set; }

        [StringLength(20)]
        public string CaozuoTimuID { get; set; }

        public int? Point { get; set; }

        public int? QuestionType { get; set; }
    }
}
