using Dashboard.Aggregator.Models;

namespace Dashboard.Aggregator.Services.Abstractions
{
    public interface IWorkoutService
    {
        Task<int> GetTotalWorkoutCount();
        Task<int> GetTotalExerciseCount();
        Task<int> GetUsersWorkoutsCount(string id);
        Task<int> GetUsersExercisesCount(string id);

    }
}
