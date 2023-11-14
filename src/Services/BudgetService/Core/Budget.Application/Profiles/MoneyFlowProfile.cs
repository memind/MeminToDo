using AutoMapper;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Domain.Entities;

namespace Budget.Application.Profiles
{
    public class MoneyFlowProfile : Profile
    {
        public MoneyFlowProfile()
        {
            CreateMap<MoneyFlow, MoneyFlowDto>().ReverseMap();
            CreateMap<MoneyFlow, MoneyFlowCreateDto>().ReverseMap();
            CreateMap<MoneyFlow, MoneyFlowUpdateDto>().ReverseMap();
        }
    }
}
