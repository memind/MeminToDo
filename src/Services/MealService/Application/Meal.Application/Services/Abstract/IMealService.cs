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



        void CreateMeal(MealCreateDto meal, Guid userId);

        MealDto UpdateMeal(MealUpdateDto meal);

        void SoftDeleteMeal(MealDeleteDto meal);

        void HardDeleteMeal(MealHardDeleteDto meal);


        Task<List<MealDto>> GetAllMealsAsync();

        Task<List<MealDto>> GetAllActiveMealsAsync();

        Task<List<MealDto>> GetAllDeletedMealsAsync();

        Task<MealDto> GetMealByIdAsync(Guid mealId);

        Task<List<MealDto>> GetUsersAllMealsAsync(Guid userId);




        Task CreateMealAsync(MealCreateDto meal, Guid userId);

        Task<MealDto> UpdateMealAsync(MealUpdateDto meal);

        Task SoftDeleteMealAsync(MealDeleteDto meal);

        Task HardDeleteMealAsync(MealHardDeleteDto meal);
    }
}
