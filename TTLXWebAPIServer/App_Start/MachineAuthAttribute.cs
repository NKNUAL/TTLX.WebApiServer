using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TTLXWebAPIServer.Api;

namespace TTLXWebAPIServer
{
    public class MachineAuthAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }
        /// <summary>  
        /// 检查用户是否有该Action执行的操作权限  
        /// </summary>  
        /// <param name="actionContext"></param>  
        public override void OnActionExecuting(HttpActionContext actionContext)
        {


            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            bool bValid = actionContext.Request.Headers.TryGetValues("x-gpcode-token", out IEnumerable<string> token);
            if (bValid)
            {
                if (token.Count() == 1)
                {
                    var service = ((BaseApiController)actionContext.ControllerContext.Controller)._verifyService;
                    if (service != null)
                    {
                        string[] tokens = token.FirstOrDefault().Split(',');
                        if (tokens.Length == 2)
                            if (service.AuthVerify(tokens[0], tokens[1]))
                                return;
                    }
                }
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new HttpResultModel
            {
                success = false,
                message = "无权限"
            });
        }
    }
}