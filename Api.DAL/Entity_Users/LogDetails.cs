namespace Api.DAL.Entity_Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LogDetails
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime LogDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string LogThread { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string LogLevel { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(4000)]
        public string LogMessage { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string MethodName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string MemberID { get; set; }
    }
}
