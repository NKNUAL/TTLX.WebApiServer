using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class VersionModel
    {
        public string name { get; set; }
        public string version { get; set; }
        public string package_url { get; set; }
    }
}