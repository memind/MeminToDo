﻿using Meal.Application.Services.Abstract;
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

        public FoodController(IFoodService foodService) => _foodService = foodService;
        

        [HttpGet]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllFoods() => _foodService.GetAllFoods();

        [HttpGet("/meal/{mealId}")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllFoodsInMeal(Guid mealId) => _foodService.GetAllFoodsInMeal(mealId);

        [HttpGet("/food/active")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllActiveFoods() => _foodService.GetAllActiveFoods();

        [HttpGet("/food/deleted")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetAllDeletedFoods() => _foodService.GetAllDeletedFoods();

        [HttpGet("/{foodId}")]
        [Authorize(Policy = "MealRead")]
        public FoodDto GetFoodById(Guid foodId) => _foodService.GetFoodById(foodId);

        [HttpGet("/user/{userId}")]
        [Authorize(Policy = "MealRead")]
        public List<FoodDto> GetUsersAllFoods(Guid userId) => _foodService.GetUsersAllFoods(userId);

        [HttpPost]
        [Authorize(Policy = "MealWrite")]
        public void CreateFood(FoodCreateDto food, Guid mealId, Guid userId) => _foodService.CreateFood(food, mealId, userId);

        [HttpPut]
        [Authorize(Policy = "MealWrite")]
        public FoodDto UpdateFood(FoodUpdateDto food) => _foodService.UpdateFood(food);

        [HttpDelete]
        [Authorize(Policy = "MealWrite")]
        public void SoftDeleteFood(FoodDeleteDto food) => _foodService.SoftDeleteFood(food);

        [HttpDelete("/hardDelete")]
        [Authorize(Policy = "MealWrite")]
        public void HardDeleteFood(FoodHardDeleteDto food) => _foodService.HardDeleteFood(food);

        [HttpGet("/consumeBackup")]
        [Authorize(Policy = "MealRead")]
        public void ConsumeBackUpInfo() => _foodService.ConsumeBackUpInfo();

        [HttpGet("/consumeTest")]
        [Authorize(Policy = "MealRead")]
        public void ConsumeTestInfo() => _foodService.ConsumeTestInfo();
    }
}
