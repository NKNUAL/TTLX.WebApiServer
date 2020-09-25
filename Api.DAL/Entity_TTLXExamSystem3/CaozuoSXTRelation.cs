namespace Api.DAL.Entity_TTLXExamSystem3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaozuoSXTRelation")]
    public partial class CaozuoSXTRelation
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CaozuoID { get; set; }

        [StringLength(50)]
        public string SXTID { get; set; }

        [StringLength(10)]
        public string SNo { get; set; }
    }
}
