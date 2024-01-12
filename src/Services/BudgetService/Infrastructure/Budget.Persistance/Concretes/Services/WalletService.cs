using AutoMapper;
using Budget.Application.Abstractions.Builders.WalletBuilder;
using Budget.Application.Abstractions.Builders.WalletBuilder.Builders;
using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Application.DTOs.WalletDTOs;
using Budget.Application.UnitOfWork;
using Budget.Persistance.Consts;
using Budget.Domain.Entities;
using Common.Caching.Services;
using Common.Logging.Logs.BudgetLogs;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using Newtonsoft.Json;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Configurations;
using Microsoft.Extensions.Options;

namespace Budget.Persistance.Concretes.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WalletService> _logger;
        private readonly IDatabase _cache;
        private readonly IMessageConsumerService _message;
        private readonly IOptions<RabbitMqUri> _rabbitMqUriConfiguration;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WalletService> logger, IMessageConsumerService message, IOptions<RabbitMqUri> rabbitMqUriConfiguration)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _message = message;

            _message.PublishConnectedInfo(MessageConsts.WalletServiceName(), _rabbitMqUriConfiguration.Value.RabbitMqHost);
            _rabbitMqUriConfiguration = rabbitMqUriConfiguration;
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
                var cacheKey = CacheConsts.GetAllWallets();
                var cachedWallets = _cache.StringGet(cacheKey);

                if (!cachedWallets.IsNull)
                    return JsonConvert.DeserializeObject<List<WalletDto>>(cachedWallets);

                var list = _unitOfWork.GetReadRepository<Wallet>().GetAll(includeProperties: w => w.BudgetAccount);
                var map = _mapper.Map<List<WalletDto>>(list);

                var serializedWallets = JsonConvert.SerializeObject(map, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                _cache.StringSet(cacheKey, serializedWallets);

                _logger.LogInformation(BudgetLogs.GetAllWallets());
                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<WalletDto> GetUsersAllWallets(Guid userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllWallets(userId);
                var cachedWallets = _cache.StringGet(cacheKey);

                if (!cachedWallets.IsNull)
                    return JsonConvert.DeserializeObject<List<WalletDto>>(cachedWallets);

                var budgetAccount = _unitOfWork.GetReadRepository<BudgetAccount>().Get(ba => ba.UserId == userId, includeProperties: ba => ba.Wallets);
                var map = _mapper.Map<List<WalletDto>>(budgetAccount.Wallets);

                var serializedWallets = JsonConvert.SerializeObject(map, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                _cache.StringSet(cacheKey, serializedWallets);

                _logger.LogInformation(BudgetLogs.GetUsersAllWallets(userId));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public WalletDto GetWalletById(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetWalletById(id);
                var cachedWallets = _cache.StringGet(cacheKey);

                if (!cachedWallets.IsNull)
                    return JsonConvert.DeserializeObject<WalletDto>(cachedWallets);

                var wallet = _unitOfWork.GetReadRepository<Wallet>().Get(w => w.Id == id, includeProperties: w => w.BudgetAccount);
                var map = _mapper.Map<WalletDto>(wallet);

                var serializedWallets = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedWallets);

                _logger.LogInformation(BudgetLogs.GetWalletById(id));

                return map;
            }
            catch (Exception error) { _logger.LogError(BudgetLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public WalletDto GetWalletByIdAsNoTracking(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetWalletByIdAsNoTracking(id);
                var cachedWallets = _cache.StringGet(cacheKey);

                if (!cachedWallets.IsNull)
                    return JsonConvert.DeserializeObject<WalletDto>(cachedWallets);

                var wallet = _unitOfWork.GetReadRepository<Wallet>().GetAsNoTracking(w => w.Id == id, includeProperties: w => w.BudgetAccount);
                var map = _mapper.Map<WalletDto>(wallet);

                var serializedWallets = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedWallets);

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

        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo(_rabbitMqUriConfiguration.Value.RabbitMqHost);

        public void ConsumeTestInfo() => _message.ConsumeStartTest(_rabbitMqUriConfiguration.Value.RabbitMqHost);
    }
}
