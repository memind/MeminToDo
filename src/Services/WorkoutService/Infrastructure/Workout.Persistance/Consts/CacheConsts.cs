namespace Workout.Persistance.Consts
{
    public static class CacheConsts
    {
        public static string GetAllWorkouts() => "GetAllWorkouts";
        public static string GetUsersAllWorkouts(string userId) => $"GetUsersAllWorkouts:{userId}";
        public static string GetWorkoutById(string workoutId) => $"GetWorkoutById:{workoutId}";


        public static string GetAllExercises() => "GetAllExercises";
        public static string GetUsersAllExercises(string userId) => $"GetUsersAllExercises:{userId}";
        public static string GetExerciseById(string exerciseId) => $"GetExerciseById:{exerciseId}";
        public static string GetAllExercisesInWorkout(string workoutId) => $"GetAllExercisesInWorkout:{workoutId}";
    }
}
