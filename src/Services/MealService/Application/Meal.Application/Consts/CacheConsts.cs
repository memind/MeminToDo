namespace Meal.Application.Consts
{
    public static class CacheConsts
    {
        public static string GetAllFoods() => "GetAllFoods";
        public static string GetAllFoodsInMeal(Guid mealId) => $"GetAllFoodsInMeal:{mealId}";
        public static string GetAllActiveFoods() => "GetAllActiveFoods";
        public static string GetAllDeletedFoods() => "GetAllDeletedFoods";
        public static string GetFoodById(Guid foodId) => $"GetFoodById:{foodId}";
        public static string GetUsersAllFoods(Guid userId) => $"GetUsersAllFoods:{userId}";
        public static string GetAllFoodHistory() => "GetAllFoodHistory";
        public static string GetFoodHistoryById(Guid historyId) => $"GetFoodHistoryById:{historyId}";
        public static string GetFoodHistoryFromTo(DateTime utcFrom, DateTime utcTo) => $"GetFoodHistoryFromTo:{utcFrom}-{utcTo}";



        public static string GetAllMeals() => "GetAllMeals";
        public static string GetAllActiveMeals() => "GetAllActiveMeals";
        public static string GetAllDeletedMeals() => "GetAllDeletedMeals";
        public static string GetMealById(Guid mealId) => $"GetMealById:{mealId}";
        public static string GetUsersAllMeals(Guid userId) => $"GetUsersAllMeals:{userId}";
        public static string GetAllMealHistory() => "GetAllMealHistory";
        public static string GetMealHistoryById(Guid historyId) => $"GetMealHistoryById:{historyId}";
        public static string GetMealHistoryFromTo(DateTime utcFrom, DateTime utcTo) => $"GetMealHistoryFromTo:{utcFrom}-{utcTo}";
    }
}
