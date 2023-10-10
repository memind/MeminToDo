using Meal.Application.Services.Abstract;
using Meal.Domain.Entities;
using Meal.Infrastructure.DTOs.FoodDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("/GetAllFoods")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllFoods()
        {
            return _foodService.GetAllFoods();
        }

        [HttpGet("/GetAllFoodsInMeal")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllFoodsInMeal(Guid mealId)
        {
            return _foodService.GetAllFoodsInMeal(mealId);
        }

        [HttpGet("/GetAllActiveFoods")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllActiveFoods()
        {
            return _foodService.GetAllActiveFoods();
        }

        [HttpGet("/GetAllDeletedFoods")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllDeletedFoods()
        {
            return _foodService.GetAllDeletedFoods();
        }

        [HttpGet("/GetFoodById")]
        [Authorize(Policy = "MealRead")]
        public FoodDto GetFoodById(Guid foodId)
        {
            return _foodService.GetFoodById(foodId);
        }

        [HttpGet("/GetUsersAllFoods")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetUsersAllFoods(Guid userId)
        {
            return _foodService.GetUsersAllFoods(userId);
        }

        [HttpPost("/CreateFood")]
        [Authorize(Policy = "MealWrite")]
        public void CreateFood(FoodCreateDto food, Guid mealId, Guid userId)
        {
            _foodService.CreateFood(food, mealId, userId);
        }

        [HttpPut("/UpdateFood")]
        [Authorize(Policy = "MealWrite")]
        public FoodDto UpdateFood(FoodUpdateDto food)
        {
            return _foodService.UpdateFood(food);
        }

        [HttpDelete("/SoftDeleteFood")]
        [Authorize(Policy = "MealWrite")]
        public void SoftDeleteFood(FoodDeleteDto food)
        {
            _foodService.SoftDeleteFood(food);
        }

        [HttpDelete("/HardDeleteFood")]
        [Authorize(Policy = "MealWrite")]
        public void HardDeleteFood(FoodHardDeleteDto food)
        {
            _foodService.HardDeleteFood(food);
        }
    }
}
