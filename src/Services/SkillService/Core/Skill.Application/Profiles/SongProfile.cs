using AutoMapper;
using Skill.Application.DTOs.SongDTOs;
using Skill.Domain.Entities;

namespace Skill.Application.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<SongDto, Song>().ReverseMap();
        }
    }
}
