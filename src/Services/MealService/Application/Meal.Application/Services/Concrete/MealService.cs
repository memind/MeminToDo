using Meal.Application.Services.Abstract;
using m = Meal.Domain.Entities;
using Meal.Infrastructure.DTOs.MealDTOs;
using Meal.Infrastructure.UnitOfWork;
using Meal.Mapper;
using Meal.Domain.Enums;
using Meal.Domain.Entities;

namespace Meal.Application.Services.Concrete
{
    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;

        public MealService(IUnitOfWork unitOfWork, ICustomMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateMeal(MealCreateDto meal, Guid userId)
        {
            var map = _mapper.Map<m.Meal, MealCreateDto>(meal);
            map.UserId = userId;

            _unitOfWork.GetWriteRepository<m.Meal>().Create(map);

            _unitOfWork.Save();
        }

        public async Task CreateMealAsync(MealCreateDto meal, Guid userId)
        {
            var map = _mapper.Map<m.Meal, MealCreateDto>(meal);
            map.UserId = userId;

            await _unitOfWork.GetWriteRepository<m.Meal>().CreateAsync(map);

            await _unitOfWork.SaveAsync();
        }

        public List<MealDto> GetAllActiveMeals()
        {
            var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public async Task<List<MealDto>> GetAllActiveMealsAsync()
        {
            var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(x => x.Status == Status.Added || x.Status == Status.Modified, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public List<MealDto> GetAllDeletedMeals()
        {
            var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(x => x.Status == Status.Deleted, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public async Task<List<MealDto>> GetAllDeletedMealsAsync()
        {
            var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(x => x.Status == Status.Deleted, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public List<MealDto> GetAllMeals()
        {
            var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public async Task<List<MealDto>> GetAllMealsAsync()
        {
            var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public MealDto GetMealById(Guid mealId)
        {
            var meals = _unitOfWork.GetReadRepository<m.Meal>().Get(x => x.Id == mealId, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map;
        }

        public async Task<MealDto> GetMealByIdAsync(Guid mealId)
        {
            var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAsync(x => x.Id == mealId, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map;
        }

        public List<MealDto> GetUsersAllMeals(Guid userId)
        {
            var meals = _unitOfWork.GetReadRepository<m.Meal>().GetAll(x => x.UserId == userId, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }

        public async Task<List<MealDto>> GetUsersAllMealsAsync(Guid userId)
        {
            var meals = await _unitOfWork.GetReadRepository<m.Meal>().GetAllAsync(x => x.UserId == userId, includeProperties: x => x.Foods);
            var map = _mapper.Map<MealDto, m.Meal>(meals);

            return map.ToList();
        }


        public void HardDeleteMeal(MealHardDeleteDto meal)
        {
            _unitOfWork.GetWriteRepository<m.Meal>().HardDelete(meal.Id);
            _unitOfWork.Save();
        }

        public async Task HardDeleteMealAsync(MealHardDeleteDto meal)
        {
            await _unitOfWork.GetWriteRepository<m.Meal>().HardDeleteAsync(meal.Id);
            await _unitOfWork.SaveAsync();
        }

        public void SoftDeleteMeal(MealDeleteDto meal)
        {
            var map = _mapper.Map<m.Meal, MealDeleteDto>(meal);

            _unitOfWork.GetWriteRepository<m.Meal>().SoftDelete(map);
            _unitOfWork.Save();
        }

        public async Task SoftDeleteMealAsync(MealDeleteDto meal)
        {
            var map = _mapper.Map<m.Meal, MealDeleteDto>(meal);

            await _unitOfWork.GetWriteRepository<m.Meal>().SoftDeleteAsync(map);
            await _unitOfWork.SaveAsync();
        }

        public MealDto UpdateMeal(MealUpdateDto meal)
        {
            var map = _mapper.Map<m.Meal, MealUpdateDto>(meal);

            _unitOfWork.GetWriteRepository<m.Meal>().Update(map);
            _unitOfWork.Save();

            return _mapper.Map<MealDto, m.Meal>(map);
        }

        public async Task<MealDto> UpdateMealAsync(MealUpdateDto meal)
        {
            var map = _mapper.Map<m.Meal, MealUpdateDto>(meal);

            await _unitOfWork.GetWriteRepository<m.Meal>().UpdateAsync(map);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MealDto, m.Meal>(map);
        }
    }
}
