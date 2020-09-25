using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class MonitorDataMdeol
    {
        public SchoolInfoModel SchoolInfo { get; set; }

        //key为专业id
        public Dictionary<string, SpecialtyDataModel> SpecialtyData { get; set; }
    }

    public class SpecialtyDataModel
    {
        public string SpecialtyCode { get; set; }

        public List<ExamDataModel> ExamData { get; set; }

        //key为练习计划id
        public Dictionary<string, ExerciseDataModel> ExerciseData { get; set; }
    }

    public class ExerciseDataModel
    {
        public string PlanName { get; set; }
        public List<PaperDataModel> PaperData { get; set; }
    }

    public class ExamDataModel
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanStartTime { get; set; }
        public string ExamTime { get; set; }//考试时长
        public PaperDataModel PaperData { get; set; }
    }

    public class PaperDataModel
    {
        public string PaperId { get; set; }

        public string PaperName { get; set; }

        public int PaperType { get; set; }

        public int TotalPeopleNum { get; set; }

        public int PeopleNum { get; set; }

        public string PaperCreateTime { get; set; }
    }
}