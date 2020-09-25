using Api.BLL;
using Api.BLL.ServiceModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Api.RenewController
{
    [RoutePrefix("api/renew")]
    [WebApiExceptionFilter]
    [MachineAuth]
    public class RenewController : BaseApiController
    {
        public RenewController(IRenewService renewService, IVerifyService verifyService)
            : base(renewService, verifyService) { }

        /// <summary>
        /// 创建订单，返回付款二维码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("getorder")]
        [HttpPost]
        public HttpResultModel Renew(RenewModel model)
        {
            var serviceModel = Mapper.Map<RenewServiceModel>(model);

            var result = _renewService.GetRenewInfo(serviceModel);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 检查续费订单状态
        /// </summary>
        /// <returns></returns>
        [Route("check")]
        [HttpPost]
        public HttpResultModel CheckRenewStatu(RenewCheckModel model)
        {
            var serviceModel = Mapper.Map<RenewCheckServiceModel>(model);

            var result = _renewService.CheckRenewStatu(serviceModel);

            return new HttpResultModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }

        /// <summary>
        /// 获取续费后的license
        /// </summary>
        /// <returns></returns>
        [Route("getlicense")]
        [HttpPost]
        public HttpResultByteModel GetRenewLicense(LicenseModel model)
        {
            var serviceModel = Mapper.Map<LicenseServiceModel>(model);

            var result = _renewService.GetRenewLicense(serviceModel);

            return new HttpResultByteModel
            {
                success = result.success,
                message = result.message,
                data = result.data
            };
        }
    }
}
