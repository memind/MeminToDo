using AutoMapper;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Application.UnitOfWork;
using Budget.Domain.Entities;
using Common.Logging.Logs.BudgetLogs;
using Microsoft.Extensions.Logging;

namespace Budget.Persistance.Concretes.Services
{
    public class BudgetAccountService : IBudgetAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BudgetAccountService> _logger;

        public BudgetAccountService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BudgetAccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateBudgetAccount(BudgetAccountDto model)
        {
            try
            {
                var map = _mapper.Map<BudgetAccount>(model);
                _unitOfWork.GetWriteRepository<BudgetAccount>().Create(map);

                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.CreateBudgetAccount(map.Id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public int DeleteBudgetAccount(Guid id)
        {
            try
            {
                _unitOfWork.GetWriteRepository<BudgetAccount>().Delete(id);
                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.DeleteBudgetAccount(id));
                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<BudgetAccountDto> GetAllBudgetAccounts()
        {
            try
            {
                var list = _unitOfWork.GetReadRepository<BudgetAccount>().GetAll(predicate: null, orderBy: null, ba => ba.MoneyFlows, ba => ba.Wallets);
                _logger.LogInformation(BudgetLogs.GetAllBudgetAccounts());

                var map = _mapper.Map<List<BudgetAccountDto>>(list);

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public BudgetAccountDto GetBudgetAccountById(Guid id)
        {
            try
            {
                var budgetAccount = _unitOfWork.GetReadRepository<BudgetAccount>().Get(ba => ba.Id == id, orderBy: null, ba => ba.MoneyFlows, ba => ba.Wallets);
                var map = _mapper.Map<BudgetAccountDto>(budgetAccount);

                _logger.LogInformation(BudgetLogs.GetBudgetAccountById(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<BudgetAccountDto> GetUsersAllBudgetAccounts(Guid id)
        {
            try
            {
                var list = _unitOfWork.GetReadRepository<BudgetAccount>().GetAll(ba => ba.UserId == id, orderBy: null, ba => ba.MoneyFlows, ba => ba.Wallets);
                var map = _mapper.Map<List<BudgetAccountDto>>(list);

                _logger.LogInformation(BudgetLogs.GetUsersAllBudgetAccounts(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public int UpdateBudgetAccount(BudgetAccountDto model)
        {
            try
            {
                var map = _mapper.Map<BudgetAccount>(model);
                _unitOfWork.GetWriteRepository<BudgetAccount>().Update(map);
                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.UpdateBudgetAccount(model.Id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }
    }
}
