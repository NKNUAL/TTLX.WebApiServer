namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpecialtyBaseRules
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SpecialtyId { get; set; }

        public int QuestionsCount { get; set; }
        public int DanxuanCount { get; set; }
        public int DuoxuanCount { get; set; }
        public int PanduanCount { get; set; }
    }
}
