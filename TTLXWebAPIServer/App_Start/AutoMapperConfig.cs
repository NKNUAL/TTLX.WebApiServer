using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTLXWebAPIServer.Mapping;

namespace TTLXWebAPIServer
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ViewModelToServiceModelProfile());
                cfg.AddProfile(new ViewModelToQueueModelProfile());
                cfg.AddProfile(new ServiceModelToQueueModelProfile());
            });
        }
    }
}