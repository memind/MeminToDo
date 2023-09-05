using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Workout.Domain.Entities.Common;

namespace Workout.Domain.Entities
{
    public class Exercise : BaseEntity
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int CurrentWeight { get; set; }
        public int Order { get; set; }

        public string WorkoutId { get; set; }

        public Workout? Workout { get; set; }
    }
}
