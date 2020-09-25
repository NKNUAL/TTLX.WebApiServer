using Api.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TTLXWebAPIServer.Api;

namespace TTLXWebAPIServer.App_Start
{
    public class MockTestAuthAttribute : ActionFilterAttribute
    {

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
            bool bValid = actionContext.Request.Headers.TryGetValues("x-mock-token", out IEnumerable<string> token);
            if (bValid)
            {
                if (token.Count() == 1)
                {
                    var verify = TokenHelper.Instance.TokenVerify(token.FirstOrDefault());
                    if (verify)
                        return;
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