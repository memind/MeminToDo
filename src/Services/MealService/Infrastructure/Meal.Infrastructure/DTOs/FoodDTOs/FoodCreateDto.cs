using Meal.Domain.Enums;

namespace Meal.Infrastructure.DTOs.FoodDTOs
{
    public class FoodCreateDto
    {
        public required string Name { get; set; }
        public int CalorieByServing { get; set; }
        public FoodCategory Category { get; set; }
    }
}
