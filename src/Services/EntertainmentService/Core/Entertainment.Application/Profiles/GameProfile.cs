using AutoMapper;
using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>().ReverseMap();
        }
    }
}