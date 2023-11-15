using AutoMapper;
using Budget.Application.Abstractions.Factories;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Application.UnitOfWork;
using Budget.Domain.Entities;

namespace Budget.Persistance.Concretes.Services
{
    public class MoneyFlowService : IMoneyFlowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMoneyFlowFactory _moneyFlowFactory;

        public MoneyFlowService(IUnitOfWork unitOfWork, IMapper mapper, IMoneyFlowFactory factory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _moneyFlowFactory = factory;
        }

        public int CreateMoneyFlow(MoneyFlowDto model)
        {
            _moneyFlowFactory.CreateMoneyFlowMessage(model);

            var map = _mapper.Map<MoneyFlow>(model);
            var account = _unitOfWork.GetReadRepository<BudgetAccount>().Get(x => x.Id == model.BudgetAccountId, includeProperties: mf => mf.MoneyFlows);

            account.MoneyFlows.Add(map);

            _unitOfWork.GetWriteRepository<MoneyFlow>().Create(map);
            _unitOfWork.GetWriteRepository<BudgetAccount>().Update(account);

            return _unitOfWork.Save();
        }

        public int DeleteMoneyFlow(Guid id)
        {
            _unitOfWork.GetWriteRepository<MoneyFlow>().Delete(id);
            return _unitOfWork.Save();
        }

        public List<MoneyFlowDto> GetAllMoneyFlows()
        {
            var list = _unitOfWork.GetReadRepository<MoneyFlow>().GetAll(includeProperties: mf => mf.BudgetAccount);
            return _mapper.Map<List<MoneyFlowDto>>(list);
        }

        public MoneyFlowDto GetMoneyFlowById(Guid id)
        {
            var moneyFlow = _unitOfWork.GetReadRepository<MoneyFlow>().Get(mf => mf.Id == id, includeProperties: mf => mf.BudgetAccount);
            var map = _mapper.Map<MoneyFlowDto>(moneyFlow);
            return map;
        }

        public MoneyFlowDto GetMoneyFlowByIdAsNoTracking(Guid id)
        {
            var moneyFlow = _unitOfWork.GetReadRepository<MoneyFlow>().GetAsNoTracking(mf => mf.Id == id, includeProperties: mf => mf.BudgetAccount);
            var map = _mapper.Map<MoneyFlowDto>(moneyFlow);
            return map;
        }

        public List<MoneyFlowDto> GetUsersAllMoneyFlows(Guid userId)
        {
            var list = _unitOfWork.GetReadRepository<MoneyFlow>().GetAll(mf => mf.UserId == userId, includeProperties: mf => mf.BudgetAccount);
            return _mapper.Map<List<MoneyFlowDto>>(list);
        }

        public int UpdateMoneyFlow(MoneyFlowDto model)
        {
            var map = _mapper.Map<MoneyFlow>(model);

            _unitOfWork.GetWriteRepository<MoneyFlow>().Update(map);
            return _unitOfWork.Save();
        }
    }
}
