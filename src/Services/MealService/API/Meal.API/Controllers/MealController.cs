using Meal.Application.Services.Abstract;
using Meal.Infrastructure.DTOs.MealDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("/GetAllMeals")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetAllMeals()
        {
            return _mealService.GetAllMeals();
        }

        [HttpGet("/GetAllActiveMeals")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetAllActiveMeals()
        {
            return _mealService.GetAllActiveMeals();
        }

        [HttpGet("/GetAllDeletedMeals")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetAllDeletedMeals()
        {
            return _mealService.GetAllDeletedMeals();
        }

        [HttpGet("/GetMealById")]
        [Authorize(Policy = "MealRead")]
        public MealDto GetMealById(Guid mealId)
        {
            return _mealService.GetMealById(mealId);
        }

        [HttpGet("GetUsersAllMeals")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetUsersAllMeals(Guid userId)
        {
            return _mealService.GetUsersAllMeals(userId);
        }

        [HttpPost("/CreateMeal")]
        [Authorize(Policy = "MealWrite")]
        public void CreateMeal(MealCreateDto meal, Guid userId)
        {
            _mealService.CreateMeal(meal, userId);
        }

        [HttpPut("/UpdateMeal")]
        [Authorize(Policy = "MealWrite")]
        public MealDto UpdateMeal(MealUpdateDto meal)
        {
            return _mealService.UpdateMeal(meal);
        }

        [HttpDelete("/SoftDeleteMeal")]
        [Authorize(Policy = "MealWrite")]
        public void SoftDeleteMeal(MealDeleteDto meal)
        {
            _mealService.SoftDeleteMeal(meal);
        }

        [HttpDelete("/HardDeleteMeal")]
        [Authorize(Policy = "MealWrite")]
        public void HardDeleteMeal(MealHardDeleteDto meal)
        {
            _mealService.HardDeleteMeal(meal);
        }
    }
}
