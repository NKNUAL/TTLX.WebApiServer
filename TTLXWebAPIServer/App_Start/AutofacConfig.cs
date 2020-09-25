using Api.BLL;
using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace TTLXWebAPIServer
{
    public class AutofacConfig
    {

        public static void AutoRegister(Type type)
        {
            var builder = new ContainerBuilder();
            //注册MVC控制器（注册所有到控制器，控制器注入，就是需要在控制器的构造函数中接收对象）
            builder.RegisterApiControllers(type.Assembly);

            Type basetype = typeof(IDependency);
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(basetype))
                .Where(t => basetype.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            //设置依赖解析器
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }


    }
}