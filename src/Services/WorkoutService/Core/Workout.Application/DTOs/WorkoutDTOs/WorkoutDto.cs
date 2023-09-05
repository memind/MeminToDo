using Workout.Application.DTOs.ExerciseDTOs;

namespace Workout.Application.DTOs.WorkoutDTOs
{
    public class WorkoutDto
    {
        public WorkoutDto()
        {
            Exercises = new List<ExerciseDto>();
        }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }

        public List<ExerciseDto> Exercises { get; set; }
    }
}
