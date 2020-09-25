namespace Api.DAL.Entity_MonitorSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UseStatusInfo_Exercise
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SchoolId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string SpecialtyCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string PlanId { get; set; }

        [StringLength(50)]
        public string PlanName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string RecordUploadTime { get; set; }
    }
}
