namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_knowledgepoint_Computer
    {
        [Key]
        [StringLength(10)]
        public string No { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(4)]
        public string FK_CourseType { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
