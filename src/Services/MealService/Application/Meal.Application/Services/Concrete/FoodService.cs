using Meal.Application.Services.Abstract;
using Meal.Domain.Entities;
using m = Meal.Domain.Entities;
using Meal.Domain.Enums;
using Meal.Infrastructure.DTOs.FoodDTOs;
using Meal.Infrastructure.DTOs.MealDTOs;
using Meal.Infrastructure.UnitOfWork;
using Meal.Mapper;
using Microsoft.Extensions.Logging;

namespace Meal.Application.Services.Concrete
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly ILogger<FoodService> _logger;

        public FoodService(IUnitOfWork unitOfWork, ICustomMapper mapper, ILogger<FoodService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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

                _logger.LogInformation($"Created Food: {food.Name}");

                _unitOfWork.Save();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
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

                _logger.LogInformation($"Created Food: {food.Name}");

                await _unitOfWork.SaveAsync();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetAllActiveFoods()
        {
            try
            {
                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);
                _logger.LogInformation($"Getting All Active Foods Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<List<FoodDto>> GetAllActiveFoodsAsync()
        {
            try
            {
                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);
                _logger.LogInformation($"Getting All Active Foods Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetAllDeletedFoods()
        {
            try
            {
                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.Status == Status.Deleted, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting All Deleted Foods Successfully.");

                return map.ToList();

            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<List<FoodDto>> GetAllDeletedFoodsAsync()
        {
            try
            {
                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.Status == Status.Deleted, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting All Deleted Foods Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetAllFoods()
        {
            try
            {
                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting All Foods Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }

        }

        public async Task<List<FoodDto>> GetAllFoodsAsync()
        {
            try
            {
                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting All Foods Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetAllFoodsInMeal(Guid mealId)
        {
            try
            {
                var meal = _unitOfWork.GetReadRepository<m.Meal>().Get(x => x.Id == mealId);

                var foods = meal.Foods.ToList();
                var map = _mapper.Map<FoodDto, Food>(foods);

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }
        public async Task<List<FoodDto>> GetAllFoodsInMealAsync(Guid mealId)
        {
            try
            {
                var meal = await _unitOfWork.GetReadRepository<m.Meal>().GetAsync(x => x.Id == mealId);

                var foods = meal.Foods.ToList();
                var map = _mapper.Map<FoodDto, Food>(foods);

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetAllHistory()
        {
            try
            {
                var history = _unitOfWork.GetReadRepository<Food>().GetHistoryAll();
                var map = _mapper.Map<FoodDto, Food>(history);

                _logger.LogInformation($"Getting All Food History Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<List<FoodDto>> GetAllHistoryAsync()
        {
            try
            {
                var history = await _unitOfWork.GetReadRepository<Food>().GetHistoryAllAsync();
                var map = _mapper.Map<FoodDto, Food>(history);

                _logger.LogInformation($"Getting All Food History Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public FoodDto GetFoodById(Guid foodId)
        {
            try
            {
                var foods = _unitOfWork.GetReadRepository<Food>().Get(x => x.Id == foodId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting Food By ID Successfully.");

                return map;
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<FoodDto> GetFoodByIdAsync(Guid foodId)
        {
            try
            {
                var foods = await _unitOfWork.GetReadRepository<Food>().GetAsync(x => x.Id == foodId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting Food By ID Successfully.");

                return map;
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetHistoryById(Guid id)
        {
            try
            {
                var history = _unitOfWork.GetReadRepository<Food>().GetHistoryAll()
                                                                   .Where(x => x.Id == id)
                                                                   .ToList();

                var map = _mapper.Map<FoodDto, Food>(history);

                _logger.LogInformation($"Getting Food History: {id}");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<List<FoodDto>> GetHistoryByIdAsync(Guid id)
        {
            try
            {
                var history = (await _unitOfWork.GetReadRepository<Food>().GetHistoryAllAsync())
                                                                   .Where(x => x.Id == id)
                                                                   .ToList();

                var map = _mapper.Map<FoodDto, Food>(history);

                _logger.LogInformation($"Getting Food History: {id}");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo)
        {
            try
            {
                var history = _unitOfWork.GetReadRepository<Food>().GetHistoryFromTo(utcFrom, utcTo);
                var map = _mapper.Map<FoodDto, Food>(history);

                _logger.LogInformation($"Getting Food History From {utcFrom} To {utcTo} Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<List<FoodDto>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo)
        {
            try
            {
                var history = await _unitOfWork.GetReadRepository<Food>().GetHistoryFromToAsync(utcFrom, utcTo);
                var map = _mapper.Map<FoodDto, Food>(history);

                _logger.LogInformation($"Getting Food History From {utcFrom} To {utcTo} Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public List<FoodDto> GetUsersAllFoods(Guid userId)
        {
            try
            {
                var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.UserId == userId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting All Foods Of User ({userId}) Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<List<FoodDto>> GetUsersAllFoodsAsync(Guid userId)
        {
            try
            {
                var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.UserId == userId, includeProperties: x => x.Meals);
                var map = _mapper.Map<FoodDto, Food>(foods);

                _logger.LogInformation($"Getting All Foods Of User ({userId}) Successfully.");

                return map.ToList();
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public void HardDeleteFood(FoodHardDeleteDto food)
        {
            try
            {
                _unitOfWork.GetWriteRepository<Food>().HardDelete(food.Id);
                _unitOfWork.Save();
                _logger.LogInformation($"Hard Deleted Food Successfully: {food.Id}");
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task HardDeleteFoodAsync(FoodHardDeleteDto food)
        {
            try
            {
                await _unitOfWork.GetWriteRepository<Food>().HardDeleteAsync(food.Id);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"Hard Deleted Food Successfully: {food.Id}");
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public void SoftDeleteFood(FoodDeleteDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodDeleteDto>(food);

                _unitOfWork.GetWriteRepository<Food>().SoftDelete(map);
                _unitOfWork.Save();

                _logger.LogInformation($"Soft Deleted Food Successfully: {food.Id}");
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task SoftDeleteFoodAsync(FoodDeleteDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodDeleteDto>(food);

                await _unitOfWork.GetWriteRepository<Food>().SoftDeleteAsync(map);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation($"Soft Deleted Food Successfully: {food.Id}");
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public FoodDto UpdateFood(FoodUpdateDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodUpdateDto>(food);

                _unitOfWork.GetWriteRepository<Food>().Update(map);
                _unitOfWork.Save();

                _logger.LogInformation($"Updated Food Successfully: {food.Id}");

                return _mapper.Map<FoodDto, Food>(map);
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }

        public async Task<FoodDto> UpdateFoodAsync(FoodUpdateDto food)
        {
            try
            {
                var map = _mapper.Map<Food, FoodUpdateDto>(food);

                await _unitOfWork.GetWriteRepository<Food>().UpdateAsync(map);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation($"Updated Food Successfully: {food.Id}");

                return _mapper.Map<FoodDto, Food>(map);
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw error; }
        }
    }
}
