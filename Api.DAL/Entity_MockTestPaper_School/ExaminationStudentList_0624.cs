namespace Api.DAL.Entity_MockTestPaper_School
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExaminationStudentList_0624
    {
        [Key]
        [Column(Order = 0)]
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

        public int? SpecialtyId { get; set; }

        public string AnswerDetail { get; set; }

        public string CaozuotiDetail { get; set; }
    }
}
