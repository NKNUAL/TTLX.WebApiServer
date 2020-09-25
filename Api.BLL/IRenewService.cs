using Api.BLL.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL
{
    public interface IRenewService : IDependency
    {
        /// <summary>
        /// 获取专业续费价格
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult GetRenewInfo(RenewServiceModel model);


        /// <summary>
        /// 检查续费状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult CheckRenewStatu(RenewCheckServiceModel model);


        /// <summary>
        /// 如果学校已经续费，则获取license文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult GetRenewLicense(LicenseServiceModel model);
    }
}
