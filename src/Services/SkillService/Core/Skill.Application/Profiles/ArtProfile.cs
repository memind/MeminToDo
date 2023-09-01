using AutoMapper;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Domain.Entities;

namespace Skill.Application.Profiles
{
    public class ArtProfile : Profile
    {
        public ArtProfile()
        {
            CreateMap<ArtDto, Art>().ReverseMap();
        }
    }
}
