using Api.Queue.QueueModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTLXWebAPIServer.Api.MockTestPaperController.Model;
using TTLXWebAPIServer.Api.Model;

namespace TTLXWebAPIServer.Mapping
{
    public class ViewModelToQueueModelProfile : Profile
    {
        public override string ProfileName => "ViewModelToQueueModelProfile";

        public ViewModelToQueueModelProfile()
        {
            CreateMap<ErrorQuestionViewModel, ErrorQuestionQueueModel>();

            CreateMap<PutQuestionModel, MockTestPaperQueueModel>();
            CreateMap<PutQuestionCourseModel, PutQuestionCourseQueueModel>();
            CreateMap<PutQuestionKnowModel, PutQuestionKnowQueueModel>();
            CreateMap<QuestionsInfoModel, QuestionsInfoQueueModel>();
        }
    }
}