using Dashboard.Aggregator.Extensions;
using System.Text.Json.Serialization;
using System.Text.Json;
using Dashboard.Aggregator.Services.Abstractions;
using Dashboard.Aggregator.Models.WorkoutModels;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class WorkoutService : IWorkoutService
    {
        private readonly HttpClient _client;

        public WorkoutService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetTotalWorkoutCount()
        {
            var response = await _client.GetAsync($"/getAllWorkouts");
            var workouts = await response.ReadContentAs<List<WorkoutModel>>();

            return workouts.Count;
        }

        public async Task<int> GetTotalExerciseCount()
        {
            var response = await _client.GetAsync($"/getAllExercises");
            var exercises = await response.ReadContentAs<List<ExerciseModel>>();

            return exercises.Count;
        }

        public async Task<int> GetUsersWorkoutsCount(string id)
        {
            var response = await _client.GetAsync($"/getUsersAllWorkouts?id={id}");
            var workouts = await response.ReadContentAs<List<WorkoutModel>>();

            return workouts.Count;
        }

        public async Task<int> GetUsersExercisesCount(string id)
        {
            var response = await _client.GetAsync($"/getUsersAllExercises?id={id}");
            var exercises = await response.ReadContentAs<List<ExerciseModel>>();

            return exercises.Count;
        }
    }
}
