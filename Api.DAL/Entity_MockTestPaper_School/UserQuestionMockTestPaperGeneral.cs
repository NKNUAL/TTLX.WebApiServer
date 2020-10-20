namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserQuestionMockTestPaperGeneral")]
    public partial class UserQuestionMockTestPaperGeneral
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string GeneralNo { get; set; }


        [StringLength(4000)]
        public string GeneralQueName { get; set; }

        [Column(TypeName = "image")]
        public byte[] NameImg { get; set; }
    }
}
