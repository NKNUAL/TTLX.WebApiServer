using Api.DAL.Entity_Server0905;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class RegisterModel
    {

        public string SchoolCode { get; set; }

        public List<SpecialtyRegInfo> RegInfos { get; set; }
    }
}