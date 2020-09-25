namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysDbType")]
    public partial class SysDbType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? QuestionDbType { get; set; }

        [StringLength(100)]
        public string QuestionDbIp { get; set; }

        [StringLength(100)]
        public string QuestionDbName { get; set; }

        [StringLength(100)]
        public string QuestionDbUserId { get; set; }

        [StringLength(100)]
        public string QuestionDbUserPwd { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}
