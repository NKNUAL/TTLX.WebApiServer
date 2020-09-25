namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GetSmsCodeHistory")]
    public partial class GetSmsCodeHistory
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ClientEndPoint { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string CreateTime { get; set; }
    }
}
