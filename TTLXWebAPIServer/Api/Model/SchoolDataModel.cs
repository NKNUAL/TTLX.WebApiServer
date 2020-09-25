using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class SchoolSimpleDataModel
    {
        public string SchoolName { get; set; }
        public string SchoolId { get; set; }
        public string FK_Province { get; set; }
        public string ProvinceName { get; set; }
        public int? ExamCount { get; set; }
        public string LastExamTime { get; set; }
        public int? ExerciseCount { get; set; }
        public string LastExerciseTime { get; set; }

    }

    public class ExamPaperMdeol
    {
        public string SchoolId { get; set; }
        public int QueryExamCount { get; set; }
        public List<QueryExamModel> QueryExam { get; set; }
    }


    public class ExercisePaperModel
    {
        public string SchoolId { get; set; }
        public int QueryExerciseCount { get; set; }
        public List<QueryExerciseModel> QueryExercise { get; set; }
    }
    
}