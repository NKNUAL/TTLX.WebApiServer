using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Api.MockTestPaperController.Model
{
    public class UserInfoModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public double DanxuanScore { get; set; }
        public double DuoxuanScore { get; set; }
        public double PanduanScore { get; set; }
        public string Token { get; set; }
    }
}