namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CloudExamRule_Computer
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string moduleName { get; set; }

        [StringLength(1000)]
        public string knowledgeList { get; set; }

        public int? ExamSpecialtyID { get; set; }

        public int? ExamCourseID { get; set; }

        public int? moduleNumDanxuan { get; set; }

        public int? moduleNumDuoxuan { get; set; }

        public int? moduleNumPanduan { get; set; }

        public int? nandu { get; set; }
    }
}
