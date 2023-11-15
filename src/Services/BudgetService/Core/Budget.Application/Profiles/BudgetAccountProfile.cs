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
        }
    }
}
