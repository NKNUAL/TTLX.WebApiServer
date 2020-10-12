namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SchoolBasicInfo")]
    public partial class SchoolBasicInfo
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string FieldName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string FieldValue { get; set; }
    }
}
