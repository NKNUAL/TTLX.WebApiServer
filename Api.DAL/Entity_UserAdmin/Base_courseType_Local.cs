namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Base_courseType_Local
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseNo { get; set; }

        [StringLength(500)]
        public string CourseName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_SpecialtyType { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string CreateUserID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string CreateUserName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string CreateTime { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
