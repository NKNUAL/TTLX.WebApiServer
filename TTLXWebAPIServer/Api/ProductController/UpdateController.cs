using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTLXWebAPIServer.Api.Model;
using TTLXWebAPIServer.Api.ProductController.Model;

namespace TTLXWebAPIServer.Api.ProductController
{
    [RoutePrefix("api/update")]
    public class UpdateController : ApiController
    {

        /// <summary>
        /// 检查版本并返回虾藻路径
        /// </summary>
        /// <param name="product_name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [Route("check")]
        [HttpGet]
        public HttpVersionResult CheckAndUpload(string product_name, string version)
        {
            string version_path = System.Web.Hosting.HostingEnvironment.MapPath("~/product_version.json");

            var versions = Newtonsoft.Json
                .JsonConvert
                .DeserializeObject<List<VersionModel>>(File.ReadAllText(version_path));

            var p_version = versions.Find(v => v.name == product_name);

            if (p_version == null)
                return new HttpVersionResult { CheckState = false };

            if (p_version.version == version)
                return new HttpVersionResult { CheckState = false };
            else
                return new HttpVersionResult
                {
                    CheckState = true,
                    PackageUrl = p_version.package_url,
                    Version = p_version.version
                };

        }

    }
}
