﻿using Meal.Infrastructure.DTOs.FoodDTOs;

namespace Meal.Application.Services.Abstract
{
    public interface IFoodService
    {
        List<FoodDto> GetAllFoods();

        List<FoodDto> GetAllActiveFoods();

        List<FoodDto> GetAllDeletedFoods();

        FoodDto GetFoodById(Guid foodId);

        List<FoodDto> GetUsersAllFoods(Guid userId);

        void CreateFood(FoodCreateDto food);

        FoodDto UpdateFood(FoodUpdateDto food);

        void SoftDeleteFood(FoodDeleteDto food);

        void HardDeleteFood(FoodHardDeleteDto food);


        Task<List<FoodDto>> GetAllFoodsAsync();

        Task<List<FoodDto>> GetAllActiveFoodsAsync();

        Task<List<FoodDto>> GetAllDeletedFoodsAsync();

        Task<FoodDto> GetFoodByIdAsync(Guid foodId);

        Task<List<FoodDto>> GetUsersAllFoodsAsync(Guid userId);

        Task CreateFoodAsync(FoodCreateDto food);

        Task<FoodDto> UpdateFoodAsync(FoodUpdateDto food);

        Task SoftDeleteFoodAsync(FoodDeleteDto food);

        Task HardDeleteFoodAsync(FoodHardDeleteDto food);
    }
}