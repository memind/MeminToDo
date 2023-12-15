namespace Common.Logging.Logs.MealLogs
{
    public static class MealLogs
    {
        public static string AnErrorOccured(string errorMessage) => $"An error occured: {errorMessage}";





        public static string CreateFood(string foodName) => $"Created Food: {foodName}";
        public static string GetAllActiveFoods() => $"Getting All Active Foods Successfully.";
        public static string GetAllDeletedFoods() => $"Getting All Deleted Foods Successfully.";
        public static string GetAllFoods() => $"Getting All Foods Successfully.";
        public static string GetAllFoodsInMeal(Guid mealId) => $"Getting All Foods In Meal: {mealId}.";
        public static string GetAllFoodHistory() => $"Getting All Food History Successfully.";
        public static string GetFoodById(Guid foodId) => $"Getting Food: {foodId}.";
        public static string GetFoodHistoryById(Guid id) => $"Getting Food History: {id}";
        public static string GetFoodHistoryFromTo(DateTime utcFrom, DateTime utcTo) => $"Getting Food History From {utcFrom} To {utcTo} Successfully.";
        public static string GetUsersAllFoods(Guid userId) => $"Getting All Foods Of User ({userId}) Successfully.";
        public static string HardDeleteFood(Guid foodId) => $"Hard Deleted Food Successfully: {foodId}";
        public static string SoftDeleteFood(Guid foodId) => $"Soft Deleted Food Successfully: {foodId}";
        public static string UpdateFood(Guid foodId) => $"Updated Food Successfully: {foodId}";





        public static string CreateMeal() => $"Created Meal Successfully.";
        public static string GetAllActiveMeals() => $"Getting All Active Meals Successfully.";
        public static string GetAllDeletedMeals() => $"Getting All Deleted Meals Successfully.";
        public static string GetAllMeals() => $"Getting All Meals Successfully.";
        public static string GetAllMealsInMeal(Guid mealId) => $"Getting All Meals In Meal: {mealId}.";
        public static string GetAllMealHistory() => $"Getting All Meal History Successfully.";
        public static string GetMealById(Guid mealId) => $"Getting Meal: {mealId}.";
        public static string GetMealHistoryById(Guid id) => $"Getting Meal History: {id}";
        public static string GetMealHistoryFromTo(DateTime utcFrom, DateTime utcTo) => $"Getting Meal History From {utcFrom} To {utcTo} Successfully.";
        public static string GetUsersAllMeals(Guid userId) => $"Getting All Meals Of User ({userId}) Successfully.";
        public static string HardDeleteMeal(Guid mealId) => $"Hard Deleted Meal Successfully: {mealId}";
        public static string SoftDeleteMeal(Guid mealId) => $"Soft Deleted Meal Successfully: {mealId}";
        public static string UpdateMeal(Guid mealId) => $"Updated Meal Successfully: {mealId}";
    }
}
