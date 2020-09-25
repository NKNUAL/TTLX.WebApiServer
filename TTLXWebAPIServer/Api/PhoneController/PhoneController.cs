using Api.BLL;
using Api.BLL.ServiceModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Api.PhoneController
{
    [RoutePrefix("api/phoneUser")]
    [WebApiExceptionFilter]
    [MachineAuth]
    public class PhoneController : BaseApiController
    {

        public PhoneController(IPhoneService phoneService, IVerifyService IverifyService)
            : base(phoneService, IverifyService) { }


        /// <summary>
        /// 获取学校某专业限制导入的人数和已经导入人数
        /// </summary>
        /// <param name="schooloNo"></param>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        [Route("limitCount/{schooloNo}")]
        [HttpGet]
        public JsonResult<HttpResultModel> GetSchoolLimitCount(string schooloNo)
        {
            var limitCount = _phoneService.GetSchoolLimitCount(schooloNo);

            List<SchoolLimitModel> limits = new List<SchoolLimitModel>();
            foreach (var item in limitCount)
            {
                limits.Add(new SchoolLimitModel
                {
                    SpecialtyId = item.SpecialtyId,
                    SpecialtyName = item.SpecialtyName,
                    LimitCount = item.LimitCount,
                    UseCount = _phoneService.GetSchoolHasUploadCount(schooloNo, int.Parse(item.SpecialtyId))
                });
            }

            return Json(new HttpResultModel { success = true, data = limits });
        }

        /// <summary>
        /// 学校上传手机用户
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [Route("upload/user")]
        [HttpPost]
        public JsonResult<HttpResultModel> UploadPhoneUser(UploadUser upload)
        {
            var uploadModel = Mapper.Map<UploadUser, UploadUserServiceModel>(upload);

            if (upload.Specialties != null)
            {
                var result = _phoneService.UploadStudent(uploadModel);
                if (result.code == 1)
                {
                    return Json(new HttpResultModel { success = true });
                }
                else
                {
                    return Json(new HttpResultModel { success = false, message = result.message });
                }
            }
            return Json(new HttpResultModel { success = false, message = "没有可上传的学生" });
        }

        [Route("{schooloNo}")]
        [HttpGet]
        public JsonResult<HttpResultModel> GetPhoneUser(string schooloNo, string specialtyCode, string studentName)
        {
            var result = _phoneService.GetPhoneUser(schooloNo, specialtyCode, studentName);
            return Json(new HttpResultModel { success = true, data = result });
        }

        [Route("editInfo")]
        [HttpPost]
        public JsonResult<HttpResultModel> EditUserInfo(UserEditInfoModel model)
        {
            var result = _phoneService.UserEditInfo(model.Lexueid, model.NewPassword, model.UserName);
            if (result.code == 0)
                return Json(new HttpResultModel { success = false, message = result.message });

            return Json(new HttpResultModel { success = true });
        }

    }
}
