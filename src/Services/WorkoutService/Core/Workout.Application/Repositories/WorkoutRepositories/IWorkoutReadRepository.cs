using Application.Repositories;
using w = Workout.Domain.Entities;

namespace Workout.Application.Repositories.WorkoutRepositories
{
    public interface IWorkoutReadRepository : IReadRepository<w.Workout>
    {
    }
}
