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

        public MealController(IMealService mealService) => _mealService = mealService;
        

        [HttpGet]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetAllMeals() => _mealService.GetAllMeals();

        [HttpGet("/meal/active")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetAllActiveMeals() => _mealService.GetAllActiveMeals();

        [HttpGet("/meal/deleted")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetAllDeletedMeals() => _mealService.GetAllDeletedMeals();

        [HttpGet("/{mealId}")]
        [Authorize(Policy = "MealRead")]
        public MealDto GetMealById(Guid mealId) => _mealService.GetMealById(mealId);

        [HttpGet("/user/{userId}")]
        [Authorize(Policy = "MealRead")]
        public List<MealDto> GetUsersAllMeals(Guid userId) => _mealService.GetUsersAllMeals(userId);

        [HttpPost]
        [Authorize(Policy = "MealWrite")]
        public void CreateMeal(MealCreateDto meal, Guid userId) => _mealService.CreateMeal(meal, userId);

        [HttpPut]
        [Authorize(Policy = "MealWrite")]
        public MealDto UpdateMeal(MealUpdateDto meal) => _mealService.UpdateMeal(meal);

        [HttpDelete]
        [Authorize(Policy = "MealWrite")]
        public void SoftDeleteMeal(MealDeleteDto meal) => _mealService.SoftDeleteMeal(meal);

        [HttpDelete("/hardDelete")]
        [Authorize(Policy = "MealWrite")]
        public void HardDeleteMeal(MealHardDeleteDto meal) => _mealService.HardDeleteMeal(meal);


        [HttpGet("/consumeBackup")]
        [Authorize(Policy = "MealRead")]
        public void ConsumeBackUpInfo() => _mealService.ConsumeBackUpInfo();

        [HttpGet("/consumeTest")]
        [Authorize(Policy = "MealRead")]
        public void ConsumeTestInfo() => _mealService.ConsumeTestInfo();
    }
}
