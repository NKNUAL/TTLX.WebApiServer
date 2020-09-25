using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class CheckPassModel
    {
        public string CheckUserId { get; set; }
        public string PaperID { get; set; }
        public int CommentLevel { get; set; }
        public string Reason { get; set; }
    }

    public class CheckRefouseModel
    {
        public string CheckUserId { get; set; }
        public string PaperID { get; set; }
        public string Reason { get; set; }
    }
}