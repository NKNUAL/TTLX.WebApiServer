using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class SchoolKVModel
    {
        public string SchoolNo { get; set; }

        public string SchoolName { get; set; }
    }


    public class SpecialtyKVModel
    {
        public string SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }
    }

    public class CourseKVModel
    {
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
    }

    public class KnowKVModel
    {
        public string KnowNo { get; set; }
        public string KnowName { get; set; }
    }
}
