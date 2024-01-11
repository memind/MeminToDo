
using Workout.Application.DTOs.ExerciseDTOs;

namespace Workout.Application.Abstractions.Services
{
    public interface IExerciseService
    {
        public List<ExerciseDto> GetAllExercises();
        public Task<List<ExerciseDto>> GetAllExercisesAsync();

        public ExerciseDto GetExerciseById(string exerciseId);
        public Task<ExerciseDto> GetExerciseByIdAsync(string exerciseId);

        public List<ExerciseDto> GetAllExercisesInWorkout(string workoutId);
        public Task<List<ExerciseDto>> GetAllExercisesInWorkoutAsync(string workoutId);

        public List<ExerciseDto> GetUsersAllExercises(string userId);
        public Task<List<ExerciseDto>> GetUsersAllExercisesAsync(string userId);


        public ExerciseDto CreateExercise(ExerciseDto model);
        public Task<ExerciseDto> CreateExerciseAsync(ExerciseDto model);

        public void DeleteExercise(string exerciseId);
        public Task DeleteExerciseAsync(string exerciseId);

        public ExerciseDto UpdateExercise(ExerciseDto model, string id);
        public Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto model, string id);

        public void ConsumeBackUpInfo();
        public void ConsumeTestInfo();
    }
}
