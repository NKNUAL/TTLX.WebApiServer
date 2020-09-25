using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class RenewModel
    {
        public int RenewType { get; set; }
        public List<string> SpecialtyIds { get; set; }
        public string SchoolNo { get; set; }
    }
}