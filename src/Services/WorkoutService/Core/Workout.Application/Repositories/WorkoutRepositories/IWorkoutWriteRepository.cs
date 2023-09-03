using Application.Repositories;
using w = Workout.Domain.Entities;

namespace Workout.Application.Repositories.WorkoutRepositories
{
    public interface IWorkoutWriteRepository : IWriteRepository<w.Workout>
    {
    }
}
