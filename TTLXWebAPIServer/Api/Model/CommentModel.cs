using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.Model
{
    public class CommentModel
    {
        public string UserToken { get; set; }
        public string PaperID { get; set; }
        public int CommentLevel { get; set; }
        public string CommentDesc { get; set; }
        public bool IsAnonymous { get; set; }
    }
}