
namespace Workout.Application.DTOs.ExerciseDTOs
{
    public class ExerciseDto
    {
        public DateTime CreatedDate { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public int CurrentWeight { get; set; }
        public int Order { get; set; }

        public Guid WorkoutId { get; set; }
    }
}
