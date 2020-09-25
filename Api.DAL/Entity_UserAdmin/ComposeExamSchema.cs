namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ComposeExamSchema")]
    public partial class ComposeExamSchema
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SchemaName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string CreateTime { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string CreateUserID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string CreateUserName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string SpecialtyId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamSchemaType { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsDelete { get; set; }
    }
}
