using Workout.Domain.Entities.Common;

namespace Workout.Domain.Entities
{
    public class Workout : BaseEntity
    {
        public Workout()
        {
            Exercises = new List<Exercise>();
        }
        public List<Exercise>? Exercises { get; set; }
    }
}
