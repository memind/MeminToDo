using Meal.Infrastructure.DTOs.FoodDTOs;

namespace Meal.Application.Services.Abstract
{
    public interface IFoodService
    {
        List<FoodDto> GetAllFoods();

        List<FoodDto> GetAllFoodsInMeal(Guid mealId);

        List<FoodDto> GetAllActiveFoods();

        List<FoodDto> GetAllDeletedFoods();

        FoodDto GetFoodById(Guid foodId);

        List<FoodDto> GetUsersAllFoods(Guid userId);

        List<FoodDto> GetAllHistory();

        List<FoodDto> GetHistoryById(Guid id);

        List<FoodDto> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo);

        void CreateFood(FoodCreateDto food, Guid mealId, Guid userId);

        FoodDto UpdateFood(FoodUpdateDto food);

        void SoftDeleteFood(FoodDeleteDto food);

        void HardDeleteFood(FoodHardDeleteDto food);


        Task<List<FoodDto>> GetAllFoodsAsync();

        Task<List<FoodDto>> GetAllFoodsInMealAsync(Guid mealId);

        Task<List<FoodDto>> GetAllActiveFoodsAsync();

        Task<List<FoodDto>> GetAllDeletedFoodsAsync();

        Task<FoodDto> GetFoodByIdAsync(Guid foodId);

        Task<List<FoodDto>> GetUsersAllFoodsAsync(Guid userId);

        Task<List<FoodDto>> GetAllHistoryAsync();

        Task<List<FoodDto>> GetHistoryByIdAsync(Guid id);

        Task<List<FoodDto>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo);

        Task CreateFoodAsync(FoodCreateDto food, Guid mealId, Guid userId);

        Task<FoodDto> UpdateFoodAsync(FoodUpdateDto food);

        Task SoftDeleteFoodAsync(FoodDeleteDto food);

        Task HardDeleteFoodAsync(FoodHardDeleteDto food);
    }
}
