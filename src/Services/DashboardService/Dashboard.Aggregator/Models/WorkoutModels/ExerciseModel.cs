namespace Dashboard.Aggregator.Models.WorkoutModels
{
    public class ExerciseModel
    {
        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int CurrentWeight { get; set; }
        public int Order { get; set; }

        public Guid WorkoutId { get; set; }
        public Guid UserId { get; set; }
    }
}
