using Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer
{
    public class Log4NetConfig
    {
        /// <summary>
        /// 重新配置log4net 改为默认数据库连接
        /// </summary>
        public static void ConfigureLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
            if (log4net.LogManager.GetRepository() is log4net.Repository.Hierarchy.Hierarchy hierarchy && hierarchy.Configured)
            {
                foreach (log4net.Appender.IAppender appender in hierarchy.GetAppenders())
                {
                    if (appender is log4net.Appender.AdoNetAppender adoNetAppender)
                    {
                        adoNetAppender.ConnectionString = ConfigTools.GetDBConnString("dbSharePaper");
                        adoNetAppender.ActivateOptions();
                    }
                }
            }
        }
    }
}