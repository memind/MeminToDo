
using Workout.Application.DTOs.ExerciseDTOs;
using Workout.Application.DTOs.WorkoutDTOs;

namespace Workout.Application.Abstractions.Services
{
    public interface IWorkoutService
    {
        public List<WorkoutDto> GetAllWorkouts();
        public Task<List<WorkoutDto>> GetAllWorkoutsAsync();

        public List<WorkoutDto> GetUsersAllWorkouts(string userId);
        public Task<List<WorkoutDto>> GetUsersAllWorkoutsAsync(string userId);

        public WorkoutDto GetWorkoutById(string workoutId);
        public Task<WorkoutDto> GetWorkoutByIdAsync(string workoutId);


        public WorkoutDto CreateWorkout(WorkoutDto model);
        public Task<WorkoutDto> CreateWorkoutAsync(WorkoutDto model);

        public void DeleteWorkout(string id);
        public Task DeleteWorkoutAsync(string id);

        public WorkoutDto UpdateWorkout(WorkoutDto model, string id);
        public Task<WorkoutDto> UpdateWorkoutAsync(WorkoutDto model, string id);
    }
}
