namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ComposeExamSchemaItem")]
    public partial class ComposeExamSchemaItem
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchemaId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueType { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string QueTypeName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string CourseId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string KnowId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string KnowName { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string Score { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DifficultLevel { get; set; }
    }
}
