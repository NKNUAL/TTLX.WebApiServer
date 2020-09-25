using Api.DAL.Entity_MonitorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class LocalQuestionsModel
    {
        public SchoolInfoModel SchoolInfo { get; set; }

        public List<Questionsinfo_Local> LocalQuestions { get; set; }
    }
}