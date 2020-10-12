namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_MonthExamSpecialty_Old
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpecialtyType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SpecialtyName { get; set; }

        [StringLength(100)]
        public string SchoolNo { get; set; }
    }
}
