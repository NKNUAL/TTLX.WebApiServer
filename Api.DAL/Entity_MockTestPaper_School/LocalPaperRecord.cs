namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocalPaperRecord")]
    public partial class LocalPaperRecord
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string PGuid { get; set; }


        public string PaperEditDate { get; set; }

        public string RuleNo { get; set; }

        public int IsNormal { get; set; }

        public string CreateUserId { get; set; }
    }
}
