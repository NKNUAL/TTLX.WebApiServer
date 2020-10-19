namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocalPaperGeneralQuestionRelation")]
    public partial class LocalPaperGeneralQuestionRelation
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string GGuid { get; set; }


        public string QGuid { get; set; }


        public int QueOrder { get; set; }


        public int QType { get; set; }
    }
}
