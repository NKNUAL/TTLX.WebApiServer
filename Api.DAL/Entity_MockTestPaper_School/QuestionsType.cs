namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionsType")]
    public partial class QuestionsType
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string TypeName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeSpecialty { get; set; }

        [StringLength(100)]
        public string TypeDesc { get; set; }

        public double? Score { get; set; }

        public int? Number { get; set; }
    }
}
