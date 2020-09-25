using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class ProvinceModel
    {
        public string FK_Province { get; set; }

        public string ProvinceName { get; set; }

    }

    public class SpecialtyModel
    {
        public string SpecialtyCode { get; set; }

        public string SpecialtyName { get; set; }
    }

    public class SchoolModel
    {
        public string SchoolId { get; set; }

        public string SchoolName { get; set; }
    }
}