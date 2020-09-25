using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class CommentQueryModel
    {
        public string PaperID { get; set; }
        public int OrderType { get; set; }
        public string OrderBy { get; set; }
    }
}