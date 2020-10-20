namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserQuestionGeneralQuestionRelation")]
    public partial class UserQuestionGeneralQuestionRelation
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string GeneralNo { get; set; }


        public string QueNo { get; set; }

        public int OrderIndex { get; set; }
    }
}
