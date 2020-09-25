using Api.BLL.ServiceModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Mapping
{
    public class ViewModelToServiceModelProfile : Profile
    {
        public override string ProfileName => "ViewModelToServiceModelProfile";

        public ViewModelToServiceModelProfile()
        {
            CreateMap<UploadUser, UploadUserServiceModel>();
            CreateMap<UploadSpecialtyModel, UploadSpecialtyServiceModel>();
            CreateMap<UploadStudentModel, UploadStudentServiceModel>();
            CreateMap<TeacherBindModel, TeacherBindServiceModel>();

            CreateMap<PaperUploadModel, PaperUploadServiceModel>();
            CreateMap<PaperQuestionsModel, PaperQuestionsServiceModel>();
            CreateMap<PaperQueryModel, PaperQueryServiceModel>();

            CreateMap<CommentModel, CommentServiceModel>();

            CreateMap<QuestionsModel, QuestionsServiceModel>();

            CreateMap<CommentQueryModel, CommentQueryServiceModel>();

            CreateMap<RenewModel, RenewServiceModel>();

            CreateMap<RenewCheckModel, RenewCheckServiceModel>();
            CreateMap<LicenseModel, LicenseServiceModel>();
        }

    }
}