using Api.BLL;
using System.Web.Http;

namespace TTLXWebAPIServer.Api
{
    public class BaseApiController : ApiController
    {
        public IPhoneService _phoneService { get; set; }
        public IVerifyService _verifyService { get; set; }
        public IShareService _shareService { get; set; }
        public IRenewService _renewService { get; set; }
        public IBaseService _baseService { get; set; }
        public BaseApiController(IRenewService renewService, IVerifyService verifyService)
        {
            _renewService = renewService;
            _verifyService = verifyService;
        }
        public BaseApiController(IPhoneService phoneService, IVerifyService verifyService)
        {
            _phoneService = phoneService;
            _verifyService = verifyService;
        }
        public BaseApiController(IShareService shareService, IVerifyService verifyService)
        {
            _shareService = shareService;
            _verifyService = verifyService;
        }

        public BaseApiController(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public BaseApiController() { }
    }
}
