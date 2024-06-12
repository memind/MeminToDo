using Dashboard.Aggregator.Extensions;
using Dashboard.Aggregator.Models.EntertainmentModels;
using Dashboard.Aggregator.Models.MealModels;
using Dashboard.Aggregator.Services.Abstractions;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class MealService : IMealService
    {
        private readonly HttpClient _client;

        public MealService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetTotalFoodCount()
        {
            var response = await _client.GetAsync($"/api/food");
            var foods = await response.ReadContentAs<List<FoodModel>>();

            return foods.Count;
        }

        public async Task<int> GetTotalMealCount()
        {
            var response = await _client.GetAsync($"/api/meal");
            var meals = await response.ReadContentAs<List<MealModel>>();

            return meals.Count;
        }

        public async Task<int> GetUsersFoodCount(string id)
        {
            var response = await _client.GetAsync($"/api/food/user/{id}");
            var foods = await response.ReadContentAs<List<FoodModel>>();

            return foods.Count;
        }

        public async Task<int> GetUsersMealCount(string id)
        {
            var response = await _client.GetAsync($"/api/meal/user/{id}");
            var meals = await response.ReadContentAs<List<MealModel>>();

            return meals.Count;
        }
    }
}
