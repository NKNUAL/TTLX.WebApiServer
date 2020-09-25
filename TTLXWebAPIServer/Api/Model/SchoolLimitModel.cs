using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class SchoolLimitModel
    {
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public int LimitCount { get; set; }
        public int UseCount { get; set; }
    }
}