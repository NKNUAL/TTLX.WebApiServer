using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class UploadUserServiceModel
    {
        public string SchoolNo { get; set; }
        public List<UploadSpecialtyServiceModel> Specialties { get; set; }
    }


    public class UploadSpecialtyServiceModel
    {
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public List<UploadStudentServiceModel> Students { get; set; }
    }


    public class UploadStudentServiceModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
    }
}
