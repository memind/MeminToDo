using m = Meal.Domain.Entities;
using Meal.Domain.Enums;
using Meal.Infrastructure.DTOs.MealDTOs;

namespace Meal.Infrastructure.DTOs.FoodDTOs
{
    public class FoodDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public required string Name { get; set; }
        public int CalorieByServing { get; set; }
        public FoodCategory Category { get; set; }

        public ICollection<m.Meal> Meals { get; set; } = new List<m.Meal>();
    }
}
