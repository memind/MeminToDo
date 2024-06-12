namespace Dashboard.Aggregator.Services.Abstractions
{
    public interface IMealService
    {
        Task<int> GetTotalMealCount();
        Task<int> GetTotalFoodCount();
        Task<int> GetUsersMealCount(string id);
        Task<int> GetUsersFoodCount(string id);

    }
}
