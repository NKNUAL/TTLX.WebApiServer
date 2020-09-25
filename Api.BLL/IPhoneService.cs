using Api.BLL.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL
{
    public interface IPhoneService : IDependency
    {
        /// <summary>
        /// 获取学校专业限制人数和已导入人数
        /// </summary>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        List<PhoneLimitModel> GetSchoolLimitCount(string schoolNo);

        /// <summary>
        /// 获取手机用户
        /// </summary>
        /// <param name="schoolNo"></param>
        /// <param name="specialtyCode"></param>
        /// <param name="studentName"></param>
        /// <returns></returns>
        List<PhoneUserModel> GetPhoneUser(string schoolNo, string specialtyCode, string studentName);

        /// <summary>
        /// 手机用户修改密码和姓名
        /// </summary>
        /// <param name="lexueid"></param>
        /// <param name="password"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        ResultModel UserEditInfo(string lexueid, string password, string userName);

        /// <summary>
        /// 手机用户上传
        /// </summary>
        /// <param name="uploadUser"></param>
        /// <returns></returns>
        ResultModel UploadStudent(UploadUserServiceModel uploadUser);

        /// <summary>
        /// 获取学校限制上传人数
        /// </summary>
        /// <param name="schoolNo"></param>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        int GetSchoolLimit(string schoolNo, string specialtyId);

        /// <summary>
        /// 获取学校已经上传人数
        /// </summary>
        /// <param name="schoolNo"></param>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        int GetSchoolHasUploadCount(string schoolNo, int specialtyId);

    }
}
