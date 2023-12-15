using AutoMapper;
using Budget.Application.Abstractions.Builders.WalletBuilder;
using Budget.Application.Abstractions.Builders.WalletBuilder.Builders;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Application.DTOs.WalletDTOs;
using Budget.Application.UnitOfWork;
using Budget.Domain.Entities;
using Common.Logging.Logs.BudgetLogs;
using Microsoft.Extensions.Logging;
using System;

namespace Budget.Persistance.Concretes.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WalletService> _logger;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WalletService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateWallet(WalletDto model)
        {
            try
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


                var wallet = director.Construct(builder, model);
                var budgetAccount = _unitOfWork.GetReadRepository<BudgetAccount>().Get(ba => ba.Id == model.BudgetAccountId, includeProperties: ba => ba.Wallets);

                var map = _mapper.Map<Wallet>(model);

                budgetAccount.Wallets.Add(map);

                _unitOfWork.GetWriteRepository<Wallet>().Create(map);
                _unitOfWork.GetWriteRepository<BudgetAccount>().Update(budgetAccount);

                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.CreateWallet(map.Id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public int DeleteWallet(Guid id)
        {
            try
            {
                _unitOfWork.GetWriteRepository<Wallet>().Delete(id);
                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.DeleteWallet(id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<WalletDto> GetAllWallets()
        {
            try
            {
                var list = _unitOfWork.GetReadRepository<Wallet>().GetAll(includeProperties: w => w.BudgetAccount);
                _logger.LogInformation(BudgetLogs.GetAllWallets());

                return _mapper.Map<List<WalletDto>>(list);
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<WalletDto> GetUsersAllWallets(Guid userId)
        {
            try
            {
                var budgetAccount = _unitOfWork.GetReadRepository<BudgetAccount>().Get(ba => ba.UserId == userId, includeProperties: ba => ba.Wallets);
                var map = _mapper.Map<List<WalletDto>>(budgetAccount.Wallets);

                _logger.LogInformation(BudgetLogs.GetUsersAllWallets(userId));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public WalletDto GetWalletById(Guid id)
        {
            try
            {
                var wallet = _unitOfWork.GetReadRepository<Wallet>().Get(w => w.Id == id, includeProperties: w => w.BudgetAccount);
                var map = _mapper.Map<WalletDto>(wallet);

                _logger.LogInformation(BudgetLogs.GetWalletById(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public WalletDto GetWalletByIdAsNoTracking(Guid id)
        {
            try
            {
                var wallet = _unitOfWork.GetReadRepository<Wallet>().GetAsNoTracking(w => w.Id == id, includeProperties: w => w.BudgetAccount);
                var map = _mapper.Map<WalletDto>(wallet);

                _logger.LogInformation(BudgetLogs.GetWalletByIdAsNoTracking(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public int UpdateWallet(WalletDto model)
        {
            try
            {
                var map = _mapper.Map<Wallet>(model);
                _unitOfWork.GetWriteRepository<Wallet>().Update(map);
                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.UpdateWallet(model.Id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }
    }
}
