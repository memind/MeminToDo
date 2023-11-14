using AutoMapper;
using Budget.Application.Abstractions.Builders.WalletBuilder;
using Budget.Application.Abstractions.Builders.WalletBuilder.Builders;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Application.DTOs.WalletDTOs;
using Budget.Application.UnitOfWork;
using Budget.Domain.Entities;
using System;

namespace Budget.Persistance.Concretes.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int CreateWallet(WalletCreateDto model)
        {
            Director director = new Director();
            IWalletBuilder builder;

            if (model.Currency == Domain.Enums.Currency.USD)
                builder = new UsdWalletBuilder();

            else if (model.Currency == Domain.Enums.Currency.EUR)
                builder = new EurWalletBuilder();

            else if (model.Currency == Domain.Enums.Currency.TL)
                builder = new TlWalletBuilder();

            else if (model.Currency == Domain.Enums.Currency.GBP)
                builder = new GbpWalletBuilder();

            else if (model.Currency == Domain.Enums.Currency.BTC)
                builder = new BtcWalletBuilder();

            else builder = new EthWalletBuilder();


            var wallet = director.Construct(builder, model.WalletName);
            var account = _unitOfWork.GetReadRepository<BudgetAccount>().Get(x => x.Id == Guid.Parse("35533212-c2cb-4a38-997a-08dbe55b146d"));

            account.Wallets.Add(wallet);

            _unitOfWork.GetWriteRepository<Wallet>().Create(wallet);
            _unitOfWork.GetWriteRepository<BudgetAccount>().Update(account);
            return _unitOfWork.Save();
        }

        public int DeleteWallet(Guid id)
        {
            _unitOfWork.GetWriteRepository<Wallet>().Delete(id);
            return _unitOfWork.Save();
        }

        public List<WalletDto> GetAllWallets()
        {
            var list = _unitOfWork.GetReadRepository<Wallet>().GetAll(includeProperties: w => w.BudgetAccount);
            return _mapper.Map<List<WalletDto>>(list);
        }

        public WalletDto GetWalletById(Guid id)
        {
            var wallet = _unitOfWork.GetReadRepository<Wallet>().Get(w => w.Id == id, includeProperties: w => w.BudgetAccount);
            return _mapper.Map<WalletDto>(wallet);
        }

        public int UpdateWallet(WalletUpdateDto model)
        {
            var map = _mapper.Map<Wallet>(model);
            _unitOfWork.GetWriteRepository<Wallet>().Update(map);

            return _unitOfWork.Save();
        }
    }
}
