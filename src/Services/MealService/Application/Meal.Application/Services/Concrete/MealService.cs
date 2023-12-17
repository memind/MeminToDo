using Meal.Application.Services.Abstract;
using m = Meal.Domain.Entities;
using Meal.Infrastructure.DTOs.MealDTOs;
using Meal.Infrastructure.UnitOfWork;
using Meal.Mapper;
using Meal.Domain.Enums;
using Meal.Domain.Entities;
using Microsoft.Extensions.Logging;
using Meal.Infrastructure.DTOs.FoodDTOs;
using Common.Logging.Logs.MealLogs;
using Common.Caching.Services;
using StackExchange.Redis;
using Meal.Application.Consts;
using Newtonsoft.Json;

namespace Meal.Application.Services.Concrete
{
    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly ILogger<MealService> _logger;
        private readonly IDatabase _cache;

        public MealService(IUnitOfWork unitOfWork, ICustomMapper mapper, ILogger<MealService> logger)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public void CreateMeal(MealCreateDto meal, Guid userId)
        {
            try
            {
                var map = _mapper.Map<m.Meal, MealCreateDto>(meal);
                map.UserId = userId;

                _unitOfWork.GetWriteRepository<m.Meal>().Create(map);

                _unitOfWork.Save();

                _logger.LogInformation(MealLogs.CreateMeal());
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task CreateMealAsync(MealCreateDto meal, Guid userId)
        {
            try
            {
                var map = _mapper.Map<m.Meal, MealCreateDto>(meal);
                map.UserId = userId;

                await _unitOfWork.GetWriteRepository<m.Meal>().CreateAsync(map);

                await _unitOfWork.SaveAsync();

                _logger.LogInformation(MealLogs.CreateMeal());
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetAllActiveMeals()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllActiveMeals();
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllActiveMeals());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetAllActiveMealsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllActiveMeals();
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals =await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllActiveMeals());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetAllDeletedMeals()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllDeletedMeals();
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(x => x.Status == Status.Deleted, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllDeletedMeals());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetAllDeletedMealsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllDeletedMeals();
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(x => x.Status == Status.Deleted, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllDeletedMeals());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetAllHistory()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllMealHistory();
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var history = _unitOfWork.GetReadRepository<m.Meal>().GetHistoryAll();
                var map = _mapper.Map<MealDto, m.Meal>(history);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllMealHistory());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetAllHistoryAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllMealHistory();
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var history = await _unitOfWork.GetReadRepository<m.Meal>().GetHistoryAllAsync();
                var map = _mapper.Map<MealDto, m.Meal>(history);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllMealHistory());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetAllMeals()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllMeals();
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllMeals());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetAllMealsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllMeals();
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetAllMeals());

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetHistoryById(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetMealHistoryById(id);
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var history = _unitOfWork.GetReadRepository<m.Meal>().GetHistoryAll()
                                                                     .Where(x => x.Id == id)
                                                                     .ToList();
                var map = _mapper.Map<MealDto, m.Meal>(history);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetMealHistoryById(id));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetHistoryByIdAsync(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetMealHistoryById(id);
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var history = (await _unitOfWork.GetReadRepository<m.Meal>().GetHistoryAllAsync())
                                                                     .Where(x => x.Id == id)
                                                                     .ToList();
                var map = _mapper.Map<MealDto, m.Meal>(history);

                var serializedMeals = JsonConvert.SerializeObject(map);
               await  _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetMealHistoryById(id));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo)
        {
            try
            {
                var cacheKey = CacheConsts.GetMealHistoryFromTo(utcFrom, utcTo);
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var history = _unitOfWork.GetReadRepository<m.Meal>().GetHistoryFromTo(utcFrom, utcTo);
                var map = _mapper.Map<MealDto, m.Meal>(history);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetMealHistoryFromTo(utcFrom, utcTo));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo)
        {
            try
            {

                var cacheKey = CacheConsts.GetMealHistoryFromTo(utcFrom, utcTo);
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var history = await _unitOfWork.GetReadRepository<m.Meal>().GetHistoryFromToAsync(utcFrom, utcTo);
                var map = _mapper.Map<MealDto, m.Meal>(history);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetMealHistoryFromTo(utcFrom, utcTo));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public MealDto GetMealById(Guid mealId)
        {
            try
            {
                var cacheKey = CacheConsts.GetMealById(mealId);
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<MealDto>(cachedMeals);

                var meals = _unitOfWork.GetReadRepository<m.Meal>().Get(x => x.Id == mealId, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetMealById(mealId));

                return map;
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<MealDto> GetMealByIdAsync(Guid mealId)
        {
            try
            {
                var cacheKey = CacheConsts.GetMealById(mealId);
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<MealDto>(cachedMeals);

                var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAsync(x => x.Id == mealId, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetMealById(mealId));

                return map;
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public List<MealDto> GetUsersAllMeals(Guid userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllMeals(userId);
                var cachedMeals = _cache.StringGet(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(x => x.UserId == userId, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetUsersAllMeals(userId));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<List<MealDto>> GetUsersAllMealsAsync(Guid userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllMeals(userId);
                var cachedMeals = await _cache.StringGetAsync(cacheKey);

                if (!cachedMeals.IsNull)
                    return JsonConvert.DeserializeObject<List<MealDto>>(cachedMeals);

                var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(x => x.UserId == userId, includeProperties: x => x.Foods);
                var map = _mapper.Map<MealDto, m.Meal>(meals);

                var serializedMeals = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedMeals);

                _logger.LogInformation(MealLogs.GetUsersAllMeals(userId));

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }


        public void HardDeleteMeal(MealHardDeleteDto meal)
        {
            try
            {
                _unitOfWork.GetWriteRepository<m.Meal>().HardDelete(meal.Id);
                _unitOfWork.Save();

                _logger.LogInformation(MealLogs.HardDeleteMeal(meal.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task HardDeleteMealAsync(MealHardDeleteDto meal)
        {
            try
            {
                await _unitOfWork.GetWriteRepository<m.Meal>().HardDeleteAsync(meal.Id);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation(MealLogs.HardDeleteMeal(meal.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public void SoftDeleteMeal(MealDeleteDto meal)
        {
            try
            {
                var map = _mapper.Map<m.Meal, MealDeleteDto>(meal);

                _unitOfWork.GetWriteRepository<m.Meal>().SoftDelete(map);
                _unitOfWork.Save();

                _logger.LogInformation(MealLogs.SoftDeleteMeal(meal.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task SoftDeleteMealAsync(MealDeleteDto meal)
        {
            try
            {
                var map = _mapper.Map<m.Meal, MealDeleteDto>(meal);

                await _unitOfWork.GetWriteRepository<m.Meal>().SoftDeleteAsync(map);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation(MealLogs.SoftDeleteMeal(meal.Id));
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public MealDto UpdateMeal(MealUpdateDto meal)
        {
            try
            {
                var map = _mapper.Map<m.Meal, MealUpdateDto>(meal);

                _unitOfWork.GetWriteRepository<m.Meal>().Update(map);
                _unitOfWork.Save();

                _logger.LogInformation(MealLogs.UpdateMeal(meal.Id));

                return _mapper.Map<MealDto, m.Meal>(map);
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }

        public async Task<MealDto> UpdateMealAsync(MealUpdateDto meal)
        {
            try
            {
                var map = _mapper.Map<m.Meal, MealUpdateDto>(meal);

                await _unitOfWork.GetWriteRepository<m.Meal>().UpdateAsync(map);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation(MealLogs.UpdateMeal(meal.Id));

                return _mapper.Map<MealDto, m.Meal>(map);
            }
            catch (Exception error) { _logger.LogError(MealLogs.AnErrorOccured(error.Message)); throw error; }
        }
    }
}
