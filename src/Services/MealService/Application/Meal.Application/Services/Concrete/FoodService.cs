using Meal.Application.Services.Abstract;
using Meal.Domain.Entities;
using m = Meal.Domain.Entities;
using Meal.Domain.Enums;
using Meal.Infrastructure.DTOs.FoodDTOs;
using Meal.Infrastructure.DTOs.MealDTOs;
using Meal.Infrastructure.UnitOfWork;
using Meal.Mapper;

namespace Meal.Application.Services.Concrete
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;

        public FoodService(IUnitOfWork unitOfWork, ICustomMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateFood(FoodCreateDto food)
        {
            var map = _mapper.Map<Food, FoodCreateDto>(food);
            _unitOfWork.GetWriteRepository<Food>().Create(map);
            _unitOfWork.Save();
        }

        public async Task CreateFoodAsync(FoodCreateDto food)
        {
            var map = _mapper.Map<Food, FoodCreateDto>(food);
            await _unitOfWork.GetWriteRepository<Food>().CreateAsync(map);
            await _unitOfWork.SaveAsync();
        }

        public List<FoodDto> GetAllActiveFoods()
        {
            var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public async Task<List<FoodDto>> GetAllActiveFoodsAsync()
        {
            var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public List<FoodDto> GetAllDeletedFoods()
        {
            var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.Status == Status.Deleted, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public async Task<List<FoodDto>> GetAllDeletedFoodsAsync()
        {
            var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.Status == Status.Deleted, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public List<FoodDto> GetAllFoods()
        {
            var foods = _unitOfWork.GetReadRepository<Food>().GetAll(includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public async Task<List<FoodDto>> GetAllFoodsAsync()
        {
            var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(includeProperties: x => x.Meals);

            var map1 = _mapper.Map<m.Meal, MealDto>(new MealDto());
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public FoodDto GetFoodById(Guid foodId)
        {
            var foods = _unitOfWork.GetReadRepository<Food>().Get(x => x.Id == foodId, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map;
        }

        public async Task<FoodDto> GetFoodByIdAsync(Guid foodId)
        {
            var foods = await _unitOfWork.GetReadRepository<Food>().GetAsync(x => x.Id == foodId, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map;
        }

        public List<FoodDto> GetUsersAllFoods(Guid userId)
        {
            var foods = _unitOfWork.GetReadRepository<Food>().GetAll(x => x.UserId == userId, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public async Task<List<FoodDto>> GetUsersAllFoodsAsync(Guid userId)
        {
            var foods = await _unitOfWork.GetReadRepository<Food>().GetAllAsync(x => x.UserId == userId, includeProperties: x => x.Meals);
            var map = _mapper.Map<FoodDto, Food>(foods);

            return map.ToList();
        }

        public void HardDeleteFood(FoodHardDeleteDto food)
        {
            _unitOfWork.GetWriteRepository<Food>().HardDelete(food.Id);
            _unitOfWork.Save();
        }

        public async Task HardDeleteFoodAsync(FoodHardDeleteDto food)
        {
            await _unitOfWork.GetWriteRepository<Food>().HardDeleteAsync(food.Id);
            await _unitOfWork.SaveAsync();
        }

        public void SoftDeleteFood(FoodDeleteDto food)
        {
            var map = _mapper.Map<Food, FoodDeleteDto>(food);

            _unitOfWork.GetWriteRepository<Food>().SoftDelete(map);
            _unitOfWork.Save();
        }

        public async Task SoftDeleteFoodAsync(FoodDeleteDto food)
        {
            var map = _mapper.Map<Food, FoodDeleteDto>(food);

            await _unitOfWork.GetWriteRepository<Food>().SoftDeleteAsync(map);
            await _unitOfWork.SaveAsync();
        }

        public FoodDto UpdateFood(FoodUpdateDto food)
        {
            var map = _mapper.Map<Food, FoodUpdateDto>(food);

            _unitOfWork.GetWriteRepository<Food>().Update(map);
            _unitOfWork.Save();

            return _mapper.Map<FoodDto, Food>(map);
        }

        public async Task<FoodDto> UpdateFoodAsync(FoodUpdateDto food)
        {
            var map = _mapper.Map<Food, FoodUpdateDto>(food);

            await _unitOfWork.GetWriteRepository<Food>().UpdateAsync(map);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<FoodDto, Food>(map);
        }
    }
}
