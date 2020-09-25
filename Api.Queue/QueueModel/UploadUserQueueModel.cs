using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.QueueModel
{
    public class UploadUserQueueModel
    {
        public string SchoolNo { get; set; }
        public List<UploadSpecialtyQueueModel> Specialties { get; set; }
    }


    public class UploadSpecialtyQueueModel
    {
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public List<UploadStudentQueueModel> Students { get; set; }
    }


    public class UploadStudentQueueModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
    }
}
