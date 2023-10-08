using Meal.Application.Services.Abstract;
using Meal.Infrastructure.DTOs.FoodDTOs;
using Meal.Infrastructure.DTOs.MealDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IFoodService _foodService;
        private IMealService _mealService;

        public TestController(IFoodService foodService, IMealService mealService)
        {
            _foodService = foodService;
            _mealService = mealService;
        }

        [HttpGet]
        public void GetTests()
        {
            var getAllFoods = _foodService.GetAllFoods();
            var getAllActiveFoods = _foodService.GetAllActiveFoods();
            var getAllDeletedFoods = _foodService.GetAllDeletedFoods();
            var getUsersAllFoods = _foodService.GetUsersAllFoods(Guid.Parse("00000000-3333-0000-0000-000000000000"));
            var getFoodById = _foodService.GetFoodById(Guid.Parse("1c86f9cf-ed1e-4e56-fd08-08dbc818882e"));


            var getAllMeals = _mealService.GetAllMeals();
            var getAllActiveMeals = _mealService.GetAllActiveMeals();
            var getAllDeletedMeals = _mealService.GetAllDeletedMeals();
            var getUsersAllMeals = _mealService.GetUsersAllMeals(Guid.Parse("00000000-3333-0000-0000-000000000000"));
            var getMealById = _mealService.GetMealById(Guid.Parse("3dce4b97-d7c1-4d53-28d0-08dbc8188ae2"));
        }

        [HttpPost("/create")]
        public void CreateFood(FoodCreateDto model)
        {
            _foodService.CreateFood(model);
        }


        [HttpPost("/update")]
        public FoodDto UpdateFood(FoodUpdateDto model)
        {
            return _foodService.UpdateFood(model);
        }


        [HttpPost("/delete")]
        public void SoftDeleteFood(FoodDeleteDto model)
        {
            _foodService.SoftDeleteFood(model);
        }

        [HttpPost("/deleteHard")]
        public void HardDeleteFood(FoodHardDeleteDto model)
        {
            _foodService.HardDeleteFood(model);
        }





        [HttpPost("/createMeal")]
        public void CreateMeal(MealCreateDto model)
        {
            _mealService.CreateMeal(model);
        }


        [HttpPost("/updateMeal")]
        public MealDto UpdateMeal(MealUpdateDto model)
        {
            return _mealService.UpdateMeal(model);
        }


        [HttpPost("/deleteMeal")]
        public void SoftDeleteMeal(MealDeleteDto model)
        {
            _mealService.SoftDeleteMeal(model);
        }

        [HttpPost("/deleteHardMeal")]
        public void HardDeleteMeal(MealHardDeleteDto model)
        {
            _mealService.HardDeleteMeal(model);
        }
    }
}
