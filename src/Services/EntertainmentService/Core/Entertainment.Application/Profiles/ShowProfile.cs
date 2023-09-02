using AutoMapper;
using Entertainment.Application.DTOs.ShowDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Profiles
{
    public class ShowProfile : Profile
    {
        public ShowProfile()
        {
            CreateMap<Show, ShowDto>().ReverseMap();
        }
    }
}