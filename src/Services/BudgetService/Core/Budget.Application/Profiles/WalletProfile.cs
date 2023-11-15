using AutoMapper;
using Budget.Application.DTOs.WalletDTOs;
using Budget.Domain.Entities;

namespace Budget.Application.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, WalletCreateDto>().ReverseMap();
            CreateMap<Wallet, WalletUpdateDto>().ReverseMap();

            CreateMap<WalletCreateDto, WalletDto>().ReverseMap();
            CreateMap<WalletUpdateDto, WalletDto>().ReverseMap();
        }
    }
}
