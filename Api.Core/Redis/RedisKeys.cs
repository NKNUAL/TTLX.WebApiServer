using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Redis
{
    public class RedisKeys
    {

        public const string GpCodeKey = "hash_school_gocpde";

        public const string MaxKaohaoKey = "MaxKaohao";

        public const string Schools = "BaseSchools";

        public const string Specialty = "Specialty";

        public const string MockToken = "token_{0}";//{0}userId

        public const string Courses = "Courses";

        public const string Know = "{0}_Know";//{0}specialtyId
    }
}
