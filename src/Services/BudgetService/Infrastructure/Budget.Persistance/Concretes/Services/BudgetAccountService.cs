using AutoMapper;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Application.UnitOfWork;
using Budget.Domain.Entities;

namespace Budget.Persistance.Concretes.Services
{
    public class BudgetAccountService : IBudgetAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BudgetAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int CreateBudgetAccount(BudgetAccountCreateDto model)
        {
            var map = _mapper.Map<BudgetAccount>(model);
            _unitOfWork.GetWriteRepository<BudgetAccount>().Create(map);

            return _unitOfWork.Save();
        }

        public int DeleteBudgetAccount(Guid id)
        {
            _unitOfWork.GetWriteRepository<BudgetAccount>().Delete(id);

            return _unitOfWork.Save();
        }

        public List<BudgetAccountDto> GetAllBudgetAccounts()
        {
            var list = _unitOfWork.GetReadRepository<BudgetAccount>().GetAll(predicate: null, orderBy: null, ba => ba.MoneyFlows, ba => ba.Wallets);
            return _mapper.Map<List<BudgetAccountDto>>(list);
        }

        public BudgetAccountDto GetBudgetAccountById(Guid id)
        {
            var budgetAccount = _unitOfWork.GetReadRepository<BudgetAccount>().Get(ba => ba.Id == id, orderBy: null, ba => ba.MoneyFlows, ba => ba.Wallets);
            return _mapper.Map<BudgetAccountDto>(budgetAccount);
        }

        public List<BudgetAccountDto> GetUsersAllBudgetAccounts(Guid id)
        {
            var list = _unitOfWork.GetReadRepository<BudgetAccount>().GetAll(ba => ba.UserId == id, orderBy: null, ba => ba.MoneyFlows, ba => ba.Wallets);
            return _mapper.Map<List<BudgetAccountDto>>(list);
        }

        public int UpdateBudgetAccount(BudgetAccountUpdateDto model)
        {
            var map = _mapper.Map<BudgetAccount>(model);
            _unitOfWork.GetWriteRepository<BudgetAccount>().Update(map);

            return _unitOfWork.Save();
        }
    }
}
