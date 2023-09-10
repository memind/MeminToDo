namespace Dashboard.Aggregator.Models.WorkoutModels
{
    public class WorkoutModel
    {
        public WorkoutModel()
        {
            Exercises = new List<ExerciseModel>();
        }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }

        public List<ExerciseModel> Exercises { get; set; }
    }
}
