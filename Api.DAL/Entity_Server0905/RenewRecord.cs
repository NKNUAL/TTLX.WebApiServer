namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RenewRecord")]
    public partial class RenewRecord
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string RenewNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string RenewDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string RenewSchoolNo { get; set; }

        [Key]
        [Column(Order = 4)]
        public double RenewPrice { get; set; }

        public int RenewStatu { get; set; }

        public byte[] RenewLicenseFile { get; set; }

        /// <summary>
        /// 续费类型：1--对公；2--对私
        /// </summary>
        public int RenewType { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadCount { get; set; }
    }
}
