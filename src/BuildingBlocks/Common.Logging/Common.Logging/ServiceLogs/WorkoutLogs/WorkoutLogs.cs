using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging.Logs.WorkoutLogs
{
    public static class WorkoutLogs
    {
        public static string AnErrorOccured(string errorMessage) => $"An error occured: {errorMessage}";





        public static string CreateExercise(string name) => $"Created Exercise: {name}";
        public static string DeleteExercise(string exerciseId) => $"Deleted Exercise: {exerciseId}";
        public static string GetAllExercises() => "Getting All Exercises";
        public static string GetAllExercisesInWorkout(string workoutId) => $"Getting All Exercises In Workout: {workoutId}";
        public static string GetExerciseById(string exerciseId) => $"Getting Exercise: {exerciseId}";
        public static string GetUsersAllExercises(string userId) => $"Getting Users All Exercises: {userId}";
        public static string UpdateExercise(string id) => $"Updated Exercise: {id}";





        public static string CreateWorkout(string name) => $"Created Workout: {name}";
        public static string DeleteWorkout(string workoutId) => $"Deleted Workout: {workoutId}";
        public static string GetAllWorkouts() => "Getting All Workouts";
        public static string GetWorkoutById(string workoutId) => $"Getting Workout: {workoutId}";
        public static string GetUsersAllWorkouts(string userId) => $"Getting Users All Workouts: {userId}";
        public static string UpdateWorkout(string id) => $"Updated Workout: {id}";
    }
}
