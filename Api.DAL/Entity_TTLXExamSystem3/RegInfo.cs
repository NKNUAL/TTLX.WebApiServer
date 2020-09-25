namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegInfo")]
    public partial class RegInfo
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string SpecialtyNo { get; set; }

        [StringLength(50)]
        public string SpecialtyName { get; set; }

        [StringLength(50)]
        public string registerCode { get; set; }

        [StringLength(50)]
        public string GpCode { get; set; }

        [StringLength(50)]
        public string CanUse { get; set; }

        [StringLength(50)]
        public string VerifyCode { get; set; }
    }
}
