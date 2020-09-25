using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class CommentServiceModel
    {
        public string UserToken { get; set; }
        public string PaperID { get; set; }
        public int CommentLevel { get; set; }
        public string CommentDesc { get; set; }
        public bool IsAnonymous { get; set; }
    }


    public class CommentsServiceModel
    {
        public int TotalCount { get; set; }
        public List<CommentDataServiceModel> CommentData { get; set; }
    }

    public class CommentDataServiceModel
    {
        public string UserName { get; set; }
        public int CommentLevel { get; set; }
        public string CommentDesc { get; set; }
        public string CommentDate { get; set; }
    }

}
