namespace Api.DAL.Entity_MockTestPaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionsReviewRecord")]
    public partial class QuestionsReviewRecord
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string QueNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OriStatus { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurStatus { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string ReviewTime { get; set; }
    }
}
