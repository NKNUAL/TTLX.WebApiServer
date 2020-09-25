namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SchoolDataUploadTime")]
    public partial class SchoolDataUploadTime
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SchoolId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string UploadTime { get; set; }
    }
}
