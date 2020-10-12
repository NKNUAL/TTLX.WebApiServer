using Api.DAL;
using Api.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer
{
    public class DbViewCreate
    {

        public static void CreateView()
        {
            DALBaseHelper helper = new DALBaseHelper();

            using (DbShareContext dbShare = new DbShareContext())
            {
                var specialties = dbShare.Base_specialtyType.ToList();

                foreach (var specialty in specialties)
                {
                    string view_name = "TotalQuestions_" + specialty.No;

                    if (!helper.CheckViewExist(dbShare, view_name))
                    {
                        string create_sql = $"create view TotalQuestions_{specialty.No}" +
                            " as" +
                            " select Name QueContent,Option0 OptionA,Option1 OptionB,Option2 OptionC,Option3 OptionD" +
                            " from [TTLXExamSystem3].[dbo]." +
                            (specialty.No == "0" ? "[QuestionsInfo_Computer_Ori]" : "[Questionsinfo_Ori]") +
                            $" where FK_SpecialtyType='{specialty.No}'" +
                            " union" +
                            " select QueContent,OptionA,OptionB,OptionC,OptionD" +
                            " from [TTLXExamSystem3].[dbo].[Questionsinfo_Recommend]" +
                            $" where FK_Specialty='{specialty.No}'" +
                            " union " +
                            "(select c.QueContent,c.Option0 OptionA,c.Option1 OptionB,c.Option2 OptionC,c.Option3 OptionD" +
                            " from PaperInfo a" +
                            " left join PaperQuestionsRelation b on a.PaperID = b.PaperID" +
                            " left join QuestionsInfo c on b.QueNo = c.No" +
                            $" where a.PaperSpecialtyId = '{specialty.No}' and a.CheckStatu=1)" +
                            $" union " +
                            $"SELECT Name QueContent,Option0 OptionA,Option1 OptionB,Option2 OptionC,Option3 OptionD FROM " +
                            $"[TTLXExamSystem3_MockTestPaper].[dbo]." +
                            (specialty.No == "0" ? "[Questionsinfo_New_Computer]" : "[Questionsinfo_New]") +
                            $"where sourcedoc='MockTestPaper'";

                        dbShare.Database.ExecuteSqlCommand(create_sql);
                    }
                }

            }
        }

    }
}