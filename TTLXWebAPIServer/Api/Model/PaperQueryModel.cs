using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class PaperQueryModel
    {
        public string SchoolNo { get; set; }
        public string SpecialtyId { get; set; }
        public string UseToken { get; set; }
        public int? PaperStatu { get; set; }
        public int? CheckStatu { get; set; }
        public int OrderType { get; set; }
        public string OrderBy { get; set; }
        public string BoughtUserToken { get; set; }
    }
}