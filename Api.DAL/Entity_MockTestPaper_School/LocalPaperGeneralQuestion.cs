namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocalPaperGeneralQuestion")]
    public partial class LocalPaperGeneralQuestion
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string GGuid { get; set; }


        public int SpecialtyId { get; set; }


        public string GeneralQueName { get; set; }


        public byte[] NameImg { get; set; }
    }
}
