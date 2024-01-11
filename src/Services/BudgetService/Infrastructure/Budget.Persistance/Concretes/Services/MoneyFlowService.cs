using AutoMapper;
using Budget.Application.Abstractions.Factories;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Application.UnitOfWork;
using Budget.Persistance.Consts;
using Budget.Domain.Entities;
using Common.Caching.Services;
using Common.Logging.Logs.BudgetLogs;
using Common.Logging.Logs.EntertainmentLogs;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.ServiceModel.Channels;
using Common.Messaging.RabbitMQ.Abstract;

namespace Budget.Persistance.Concretes.Services
{
    public class MoneyFlowService : IMoneyFlowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMoneyFlowFactory _moneyFlowFactory;
        private readonly ILogger<MoneyFlowService> _logger;
        private readonly IDatabase _cache;
        private readonly IMessageConsumerService _message;

        public MoneyFlowService(IUnitOfWork unitOfWork, IMapper mapper, IMoneyFlowFactory factory, ILogger<MoneyFlowService> logger, IMessageConsumerService message)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _moneyFlowFactory = factory;
            _logger = logger;
            _message = message;

            _message.PublishConnectedInfo(MessageConsts.MoneyFlowServiceName());
        }

        public int CreateMoneyFlow(MoneyFlowDto model)
        {
            try
            {
                _moneyFlowFactory.CreateMoneyFlowMessage(model);

                var map = _mapper.Map<MoneyFlow>(model);
                var account = _unitOfWork.GetReadRepository<BudgetAccount>().Get(x => x.Id == model.BudgetAccountId, includeProperties: mf => mf.MoneyFlows);

                account.MoneyFlows.Add(map);

                _unitOfWork.GetWriteRepository<MoneyFlow>().Create(map);
                _unitOfWork.GetWriteRepository<BudgetAccount>().Update(account);

                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.CreateMoneyFlow(map.Id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public int DeleteMoneyFlow(Guid id)
        {
            try
            {
                _unitOfWork.GetWriteRepository<MoneyFlow>().Delete(id);
                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.DeleteMoneyFlow(id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MoneyFlowDto> GetAllMoneyFlows()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllMoneyFlows();
                var cachedMoneyFlows = _cache.StringGet(cacheKey);

                if (!cachedMoneyFlows.IsNull)
                    return JsonConvert.DeserializeObject<List<MoneyFlowDto>>(cachedMoneyFlows);

                var list = _unitOfWork.GetReadRepository<MoneyFlow>().GetAll(includeProperties: mf => mf.BudgetAccount);
                var map = _mapper.Map<List<MoneyFlowDto>>(list);

                var serializedMoneyFlows = JsonConvert.SerializeObject(map, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                _cache.StringSet(cacheKey, serializedMoneyFlows);

                _logger.LogInformation(BudgetLogs.GetAllMoneyFlows());

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public MoneyFlowDto GetMoneyFlowById(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetMoneyFlowById(id);
                var cachedMoneyFlows = _cache.StringGet(cacheKey);

                if (!cachedMoneyFlows.IsNull)
                    return JsonConvert.DeserializeObject<MoneyFlowDto>(cachedMoneyFlows);

                var moneyFlow = _unitOfWork.GetReadRepository<MoneyFlow>().Get(mf => mf.Id == id, includeProperties: mf => mf.BudgetAccount);
                var map = _mapper.Map<MoneyFlowDto>(moneyFlow);

                var serializedMoneyFlows = JsonConvert.SerializeObject(map, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                _cache.StringSet(cacheKey, serializedMoneyFlows);

                _logger.LogInformation(BudgetLogs.GetMoneyFlowById(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public MoneyFlowDto GetMoneyFlowByIdAsNoTracking(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetMoneyFlowByIdAsNoTracking(id);
                var cachedMoneyFlows = _cache.StringGet(cacheKey);

                if (!cachedMoneyFlows.IsNull)
                    return JsonConvert.DeserializeObject<MoneyFlowDto>(cachedMoneyFlows);

                var moneyFlow = _unitOfWork.GetReadRepository<MoneyFlow>().GetAsNoTracking(mf => mf.Id == id, includeProperties: mf => mf.BudgetAccount);
                var map = _mapper.Map<MoneyFlowDto>(moneyFlow);

                var serializedMoneyFlows = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMoneyFlows);

                _logger.LogInformation(BudgetLogs.GetMoneyFlowByIdAsNoTracking(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MoneyFlowDto> GetUsersAllMoneyFlows(Guid userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllMoneyFlows(userId);
                var cachedMoneyFlows = _cache.StringGet(cacheKey);

                if (!cachedMoneyFlows.IsNull)
                    return JsonConvert.DeserializeObject<List<MoneyFlowDto>>(cachedMoneyFlows);

                var list = _unitOfWork.GetReadRepository<MoneyFlow>().GetAll(mf => mf.UserId == userId, includeProperties: mf => mf.BudgetAccount);
                var map = _mapper.Map<List<MoneyFlowDto>>(list);

                var serializedMoneyFlows = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMoneyFlows);

                _logger.LogInformation(BudgetLogs.GetUsersAllMoneyFlows(userId));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public int UpdateMoneyFlow(MoneyFlowDto model)
        {
            try
            {
                var map = _mapper.Map<MoneyFlow>(model);

                _unitOfWork.GetWriteRepository<MoneyFlow>().Update(map);
                int returnValue = _unitOfWork.Save();

                _logger.LogInformation(BudgetLogs.UpdateMoneyFlow(model.Id));

                return returnValue;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo();

        public void ConsumeTestInfo() => _message.ConsumeStartTest();
    }
}
