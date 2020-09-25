namespace Api.DAL.Entity_SharePaper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentRecord")]
    public partial class CommentRecord
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string CommentNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string CommentDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string PaperID { get; set; }


        public int CommentLevel { get; set; }


        public string CommentDesc { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string CommentUserId { get; set; }


        public int IsAnonymous { get; set; }

        [Column(Order = 8)]
        public int IsDelete { get; set; }
    }
}
