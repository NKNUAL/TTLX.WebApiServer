namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MockTestPaperAuth")]
    public partial class MockTestPaperAuth
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Lexueid { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(4)]
        public string SpecialtyType { get; set; }

        [StringLength(100)]
        public string SpecialtyName { get; set; }

    }
}
