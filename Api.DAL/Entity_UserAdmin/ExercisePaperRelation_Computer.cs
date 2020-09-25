namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExercisePaperRelation_Computer
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ExerciseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string PaperID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string PaperName { get; set; }
    }
}
