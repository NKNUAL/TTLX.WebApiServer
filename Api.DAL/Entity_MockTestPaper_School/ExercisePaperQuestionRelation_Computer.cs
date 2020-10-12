namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExercisePaperQuestionRelation_Computer
    {
        public int? PaperID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string QuestionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Point { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionType { get; set; }
    }
}
