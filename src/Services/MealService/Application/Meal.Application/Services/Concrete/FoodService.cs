﻿using Meal.Application.Services.Abstract;
using Meal.Domain.Entities;
using m = Meal.Domain.Entities;
using Meal.Domain.Enums;
using Meal.Infrastructure.DTOs.FoodDTOs;
using Meal.Infrastructure.DTOs.MealDTOs;
using Meal.Infrastructure.UnitOfWork;
using Meal.Mapper;
using Microsoft.Extensions.Logging;
using Common.Logging.Logs.MealLogs;
using StackExchange.Redis;
using Common.Caching.Services;
using Meal.Application.Consts;
using Newtonsoft.Json;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Configurations;
using Microsoft.Extensions.Options;

namespace Meal.Application.Services.Concrete
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly ILogger<FoodService> _logger;
        private readonly IDatabase _cache;
        private readonly IMessageConsumerService _message;
        private readonly IOptions<RabbitMqUri> _rabbitMqUriConfiguration;

        public FoodService(IUnitOfWork unitOfWork, ICustomMapper mapper, ILogger<FoodService> logger, IMessageConsumerService message, IOptions<RabbitMqUri> rabbitMqUriConfiguration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _message = message;
            _rabbitMqUriConfiguration = rabbitMqUriConfiguration;

            _message.PublishConnectedInfo(MessageConsts.FoodServiceName(), _rabbitMqUriConfiguration.Value.RabbitMqHost);
            _cache = RedisService.GetRedisMasterDatabase();
        }

        public void CreateFood(FoodCreateDto food, Guid mealId, Guid userId)
        {
            try
            {
                var map = _mapper.Map<Food, FoodCreateDto>(food);
                var meal = _unitOfWork.GetReadRepository<m.Meal>().Get(x => x.Id == mealId);

                meal.Foods.Add(map);
                map.UserId = userId;

                _unitOfWork.GetWriteRepository<Food>().Create(map);
                _unitOfWork.GetWriteRepository<m.Meal>().Update(meal);

                _logger.LogInformation(MealLogs.CreateFood(food.Name));

                _unitOfWork.Save();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task CreateFoodAsync(FoodCreateDto food, Guid mealId, Guid userId)
        {
            try
            {
                var map = _mapper.Map<Food, FoodCreateDto>(food);
                var meal = await _unitOfWork.GetReadRepository<m.Meal>().GetAsync(x => x.Id == mealId);

                meal.Foods.Add(map);
                map.UserId = userId;

                await _unitOfWork.GetWriteRepository<Food>().CreateAsync(map);
                await _unitOfWork.GetWriteRepository<m.Meal>().UpdateAsync(meal);

                _logger.LogInformation(MealLogs.CreateFood(food.Name));

                await _unitOfWork.SaveAsync();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetAllActiveFoods()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllActiveFoods();
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetAllActiveFoods());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetAllActiveFoodsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllActiveFoods();
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetAllActiveFoods());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetAllDeletedFoods()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllDeletedFoods();
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.Status == Status.Deleted, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);

                _cache.StringSet(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetAllDeletedFoods());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetAllDeletedFoodsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllDeletedFoods();
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.Status == Status.Deleted, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);

                await _cache.StringSetAsync(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetAllDeletedFoods());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetAllFoods()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllFoods();
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetAllFoods());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }

        }

        public async Task<List<FoodDto>> GetAllFoodsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllFoods();
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetAllFoods());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetAllFoodsInMeal(Guid mealId)
        {
            try
            {
                var cacheKey = CacheConsts.GetAllFoodsInMeal(mealId);
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var meal = _unitOfWork.GetReadRepository<m.Meal>().Get(x => x.Id == mealId);

                var foods = meal.Foods.ToList();
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetAllFoodsInMeal(mealId));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetAllFoodsInMealAsync(Guid mealId)
        {
            try
            {
                var cacheKey = CacheConsts.GetAllFoodsInMeal(mealId);
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var meal = await _unitOfWork.GetReadRepository<m.Meal>().GetAsync(x => x.Id == mealId);

                var foods = meal.Foods.ToList();
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetAllFoodsInMeal(mealId));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetAllHistory()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllFoodHistory();
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var history = _unitOfWork.GetReadRepository<Food>().GetHistoryAll();
                var map = _mapper.Map<FoodDto, Food>(history);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetAllFoodHistory());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetAllHistoryAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllFoodHistory();
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var history = await _unitOfWork.GetReadRepository<Food>().GetHistoryAllAsync();
                var map = _mapper.Map<FoodDto, Food>(history);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetAllFoodHistory());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public FoodDto GetFoodById(Guid foodId)
        {
            try
            {
                var cacheKey = CacheConsts.GetFoodById(foodId);
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<FoodDto>(cachedFoods);

                var foods = _unitOfWork.GetReadRepository<Food>().Get(x => x.Id == foodId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetFoodById(foodId));

                return map;
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<FoodDto> GetFoodByIdAsync(Guid foodId)
        {
            try
            {
                var cacheKey = CacheConsts.GetFoodById(foodId);
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<FoodDto>(cachedFoods);

                var foods = await _unitOfWork.GetReadRepository<Food>().GetAsync(x => x.Id == foodId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetFoodById(foodId));

                return map;
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetHistoryById(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetFoodHistoryById(id);
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var history = _unitOfWork.GetReadRepository<Food>().GetHistoryAll()
                                                                   .Where(x => x.Id == id)
                                                                   .ToList();
                var map = _mapper.Map<FoodDto, Food>(history);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetFoodHistoryById(id));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetHistoryByIdAsync(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetFoodHistoryById(id);
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var history = (await _unitOfWork.GetReadRepository<Food>().GetHistoryAllAsync())
                                                                   .Where(x => x.Id == id)
                                                                   .ToList();
                var map = _mapper.Map<FoodDto, Food>(history);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetFoodHistoryById(id));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo)
        {
            try
            {
                var cacheKey = CacheConsts.GetFoodHistoryFromTo(utcFrom, utcTo);
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var history = _unitOfWork.GetReadRepository<Food>().GetHistoryFromTo(utcFrom, utcTo);
                var map = _mapper.Map<FoodDto, Food>(history);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetFoodHistoryFromTo(utcFrom, utcTo));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo)
        {
            try
            {
                var cacheKey = CacheConsts.GetFoodHistoryFromTo(utcFrom, utcTo);
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var history = await _unitOfWork.GetReadRepository<Food>().GetHistoryFromToAsync(utcFrom, utcTo);
                var map = _mapper.Map<FoodDto, Food>(history);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);


                _logger.LogInformation(MealLogs.GetFoodHistoryFromTo(utcFrom, utcTo));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<FoodDto> GetUsersAllFoods(Guid userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllFoods(userId);
                var cachedFoods = _cache.StringGet(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.UserId == userId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetUsersAllFoods(userId));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<FoodDto>> GetUsersAllFoodsAsync(Guid userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllFoods(userId);
                var cachedFoods = await _cache.StringGetAsync(cacheKey);

                if (!cachedFoods.IsNull)
                    return JsonConvert.DeserializeObject<List<FoodDto>>(cachedFoods);

                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.UserId == userId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                var serializedFoods = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedFoods);

                _logger.LogInformation(MealLogs.GetUsersAllFoods(userId));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public void HardDeleteFood(FoodHardDeleteDto food)
        {
            try
            {
                _unitOfWork.GetWriteRepository<Food>().HardDelete(food.Id);
                _unitOfWork.Save();
                _logger.LogInformation(MealLogs.HardDeleteFood(food.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task HardDeleteFoodAsync(FoodHardDeleteDto food)
        {
            try
            {
                await _unitOfWork.GetWriteRepository<Food>().HardDeleteAsync(food.Id);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation(MealLogs.HardDeleteFood(food.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public void SoftDeleteFood(FoodDeleteDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodDeleteDto>(food);

                _unitOfWork.GetWriteRepository<Food>().SoftDelete(map);
                _unitOfWork.Save();

                _logger.LogInformation(MealLogs.SoftDeleteFood(food.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task SoftDeleteFoodAsync(FoodDeleteDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodDeleteDto>(food);

                await _unitOfWork.GetWriteRepository<Food>().SoftDeleteAsync(map);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation(MealLogs.SoftDeleteFood(food.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public FoodDto UpdateFood(FoodUpdateDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodUpdateDto>(food);

                _unitOfWork.GetWriteRepository<Food>().Update(map);
                _unitOfWork.Save();

                _logger.LogInformation(MealLogs.UpdateFood(food.Id));

                return _mapper.Map<FoodDto, Food>(map);
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<FoodDto> UpdateFoodAsync(FoodUpdateDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodUpdateDto>(food);

                await _unitOfWork.GetWriteRepository<Food>().UpdateAsync(map);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation(MealLogs.UpdateFood(food.Id));

                return _mapper.Map<FoodDto, Food>(map);
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo(_rabbitMqUriConfiguration.Value.RabbitMqHost);

        public void ConsumeTestInfo() => _message.ConsumeStartTest(_rabbitMqUriConfiguration.Value.RabbitMqHost);
    }
}
