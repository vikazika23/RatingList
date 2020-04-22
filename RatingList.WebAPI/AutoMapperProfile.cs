using AutoMapper;
using Media.Client.DTO.Read;
using Media.Client.Requests.Create;
using Media.Client.Requests.Update;
using Media.Domain.Models;

namespace Media.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<DataAccess.Entities.Rating, Domain.Rating>();
            this.CreateMap<DataAccess.Entities.Viewer, Domain.Viewer>();
            this.CreateMap<DataAccess.Entities.Critic, Domain.Critic>();

            this.CreateMap<Domain.Rating, RatingDTO>();
            this.CreateMap<Domain.Viewer, ViewerDTO>();
            this.CreateMap<Domain.Critic, CriticDTO>();

            this.CreateMap<RatingCreateDTO, RatingUpdateModel>();
            this.CreateMap<RatingUpdateDTO, RatingUpdateModel>();
            this.CreateMap<RatingUpdateModel, DataAccess.Entities.Rating>();
            
            this.CreateMap<ViewerCreateDTO, ViewerUpdateModel>();
            this.CreateMap<ViewerUpdateDTO, ViewerUpdateModel>();
            this.CreateMap<ViewerUpdateModel, DataAccess.Entities.Viewer>();

            this.CreateMap<CriticCreateDTO, CriticUpdateModel>();
            this.CreateMap<CriticUpdateDTO, CriticUpdateModel>();
            this.CreateMap<CriticUpdateModel, DataAccess.Entities.Critic>();
        }
    }
}