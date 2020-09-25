namespace Api.DAL.Entity_Server0905
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServerMachineRegInfo")]
    public partial class ServerMachineRegInfo
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SchoolCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string SchoolName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string MachineCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UseStatus { get; set; }
    }
}
