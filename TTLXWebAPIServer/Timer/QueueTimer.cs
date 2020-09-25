using Api.BLL.Helper;
using Api.Queue;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Helper
{
    public class QueueTimer : Registry
    {

        public QueueTimer()
        {
            // Schedule an IJob to run at an interval
            // 立即执行每两秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）

            Schedule(() =>
            {
                //数据上传
                GlabolDataExe.Instance.Process();
                //共享试卷订单处理
                OrderHelper.Instance.Process();
                //续费订单处理
                RenewOrderHelper.Instance.Process();

            }).ToRunNow().AndEvery(1).Minutes();

        }





    }
}