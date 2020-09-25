using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{

    public class QueryExamModel
    {
        public string SpecialtyCode { get; set; }
        public string TeacherName { get; set; }
        public string PlanName { get; set; }
        public string PlanStartTime { get; set; }
        public string PaperName { get; set; }
        public int PaperType { get; set; }
        public int? TotalPeopleNum { get; set; }
        public int? PeopleNum { get; set; }
        public string PaperCreateTime { get; set; }
    }

    public class QueryExerciseModel
    {
        public string SpecialtyCode { get; set; }
        public string PlanName { get; set; }
        public string PaperName { get; set; }
        public int PaperType { get; set; }
        public int? TotalPeopleNum { get; set; }
        public int? PeopleNum { get; set; }
        public string PaperCreateTime { get; set; }
    }


    public class QueryPaperModel
    {
        public string SchoolId { get; set; }

        public string SpecialtyCode { get; set; }

        /// <summary>
        /// 组卷方式
        /// </summary>
        public int? PaperType { get; set; }

        public string PlanStartTime { get; set; }

        public string PlanEndTime { get; set; }

        public int StartIndex { get; set; }

        public int Page { get; set; }
    }
}