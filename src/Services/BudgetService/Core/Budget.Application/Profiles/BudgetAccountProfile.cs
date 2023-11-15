using AutoMapper;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Domain.Entities;

namespace Budget.Application.Profiles
{
    public class BudgetAccountProfile : Profile
    {
        public BudgetAccountProfile()
        {
            CreateMap<BudgetAccount, BudgetAccountDto>().ReverseMap();
            CreateMap<BudgetAccount, BudgetAccountCreateDto>().ReverseMap();
            CreateMap<BudgetAccount, BudgetAccountUpdateDto>().ReverseMap();

            CreateMap<BudgetAccountCreateDto, BudgetAccountDto>().ReverseMap();
            CreateMap<BudgetAccountUpdateDto, BudgetAccountDto>().ReverseMap();
        }
    }
}
