using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class UploadUser
    {
        public string SchoolNo { get; set; }
        public List<UploadSpecialtyModel> Specialties { get; set; }
    }



    public class UploadSpecialtyModel
    {
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public List<UploadStudentModel> Students { get; set; }
    }


    public class UploadStudentModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
    }
}