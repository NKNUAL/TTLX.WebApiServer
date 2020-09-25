using Api.Core.Logger;
using Api.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TTLXWebAPIServer.Controllers
{
    [WebApiExceptionFilter]
    public class TestController : ApiController
    {
        [HttpGet]
        public bool Test()
        {
            return true;
        }
        [HttpGet]
        public bool TestConn()
        {
            LogContent.Instance.WriteLog(new AppOpLog
            {
                MemberID = "TestConn",
                LogMessage = "调用测试接口",
                MethodName = "[SchoolController.TestConn()]"
            }, Log4NetLevel.Info);
            return true;
        }

        [HttpGet]
        public string Test(string pid)
        {
            string result = string.Empty;
            using (DbMonitorSystemContext db = new DbMonitorSystemContext())
            {
                try
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "Test",
                        LogMessage = "调用测试接口",
                        MethodName = "[SchoolController.Test()]"
                    }, Log4NetLevel.Info);
                    var province = db.Base_Province.Where(p => p.No == pid).FirstOrDefault();
                    result = province.No + "-" + province.Name + "-" + province.TotalName;
                }
                catch (Exception ex)
                {
                    LogContent.Instance.WriteLog(new AppOpLog
                    {
                        MemberID = "Test",
                        LogMessage = ex.Message,
                        MethodName = "[SchoolController.Test()]"
                    }, Log4NetLevel.Error);
                }

            }
            return result;
        }


        [HttpGet]
        public HttpResponseMessage Test2()
        {
            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.Moved);
            resp.Headers.Location = new Uri("https://qr.alipay.com/bax03760mk83iwareey45078");
            return resp;
        }
    }
}
