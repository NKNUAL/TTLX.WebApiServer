namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExaminationStudentList_query
    {
        [Key]
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string StudentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KsjhId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string StudentName { get; set; }

        [StringLength(100)]
        public string StudentPCAddress { get; set; }

        [StringLength(100)]
        public string ExamRstartTime { get; set; }

        [StringLength(100)]
        public string ExamEndTime { get; set; }

        [StringLength(100)]
        public string StudentScore { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamStatus { get; set; }
    }
}
