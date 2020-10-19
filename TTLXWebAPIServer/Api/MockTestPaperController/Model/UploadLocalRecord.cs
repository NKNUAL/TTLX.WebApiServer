using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class UploadLocalRecord
    {
        public string UserId { get; set; }
        public int SpecialtyId { get; set; }
        public List<EditPaperRecord> Papers { get; set; }
    }
}