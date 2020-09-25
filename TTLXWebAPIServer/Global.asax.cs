using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TTLXWebAPIServer.Helper;

namespace TTLXWebAPIServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册webapi
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //注册定时器
            JobManager.Initialize(new QueueTimer());
            //加载log4net的数据库连接
            Log4NetConfig.ConfigureLog4Net();
            //注册autofac
            AutofacConfig.AutoRegister(typeof(WebApiApplication));
            //配置映射
            AutoMapperConfig.Configure();

            //创建视图
            DbViewCreate.CreateView();
        }
    }
}
