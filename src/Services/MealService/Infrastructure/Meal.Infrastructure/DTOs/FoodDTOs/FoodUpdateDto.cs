using Meal.Domain.Enums;

namespace Meal.Infrastructure.DTOs.FoodDTOs
{
    public class FoodUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int CalorieByServing { get; set; }
        public FoodCategory Category { get; set; }
    }
}
