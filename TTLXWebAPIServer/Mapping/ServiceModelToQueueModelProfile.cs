using Api.BLL.ServiceModel;
using Api.Queue.QueueModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTLXWebAPIServer.Mapping
{
    public class ServiceModelToQueueModelProfile : Profile
    {
        public override string ProfileName => "ServiceModelToQueueModelProfile";

        public ServiceModelToQueueModelProfile()
        {
            CreateMap<UploadUserServiceModel, UploadUserQueueModel>();
            CreateMap<UploadSpecialtyServiceModel, UploadSpecialtyQueueModel>();
            CreateMap<UploadStudentServiceModel, UploadStudentQueueModel>();

            CreateMap<PaperUploadServiceModel, PaperUploadQueueModel>();
            CreateMap<PaperQuestionsServiceModel, PaperQuestionsQueueModel>();
        }
    }
}