namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserTable_SchoolUpload
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string UserPassword { get; set; }

        public int? FK_Specialty { get; set; }

        [StringLength(100)]
        public string FK_SpecialtyName { get; set; }

        [StringLength(100)]
        public string FK_School { get; set; }

        [StringLength(100)]
        public string FK_SchoolID { get; set; }

        [StringLength(100)]
        public string UserClass { get; set; }

        [StringLength(100)]
        public string UserClassCode { get; set; }

        [StringLength(50)]
        public string UploadDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfirmStatu { get; set; }
    }
}
