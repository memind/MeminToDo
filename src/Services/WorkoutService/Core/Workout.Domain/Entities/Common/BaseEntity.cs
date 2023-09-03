using System.Text.Json.Serialization;

namespace Workout.Domain.Entities.Common
{
    public class BaseEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

    }
}
