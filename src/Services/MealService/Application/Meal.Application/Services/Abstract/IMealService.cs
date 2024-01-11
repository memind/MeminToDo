using Meal.Infrastructure.DTOs.FoodDTOs;
using Meal.Infrastructure.DTOs.MealDTOs;
using System.Linq.Expressions;
using e = Meal.Domain.Entities;

namespace Meal.Application.Services.Abstract
{
    public interface IMealService
    {
        List<MealDto> GetAllMeals();

        List<MealDto> GetAllActiveMeals();

        List<MealDto> GetAllDeletedMeals();

        MealDto GetMealById(Guid mealId);

        List<MealDto> GetUsersAllMeals(Guid userId);

        List<MealDto> GetAllHistory();

        List<MealDto> GetHistoryById(Guid id);

        List<MealDto> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo);



        void CreateMeal(MealCreateDto meal, Guid userId);

        MealDto UpdateMeal(MealUpdateDto meal);

        void SoftDeleteMeal(MealDeleteDto meal);

        void HardDeleteMeal(MealHardDeleteDto meal);


        Task<List<MealDto>> GetAllMealsAsync();

        Task<List<MealDto>> GetAllActiveMealsAsync();

        Task<List<MealDto>> GetAllDeletedMealsAsync();

        Task<MealDto> GetMealByIdAsync(Guid mealId);

        Task<List<MealDto>> GetUsersAllMealsAsync(Guid userId);

        Task<List<MealDto>> GetAllHistoryAsync();

        Task<List<MealDto>> GetHistoryByIdAsync(Guid id);

        Task<List<MealDto>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo);




        Task CreateMealAsync(MealCreateDto meal, Guid userId);

        Task<MealDto> UpdateMealAsync(MealUpdateDto meal);

        Task SoftDeleteMealAsync(MealDeleteDto meal);

        Task HardDeleteMealAsync(MealHardDeleteDto meal);





        public void ConsumeBackUpInfo();

        public void ConsumeTestInfo();
    }
}
