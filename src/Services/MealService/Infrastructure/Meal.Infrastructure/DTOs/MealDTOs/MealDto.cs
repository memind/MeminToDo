using Meal.Domain.Entities;
using Meal.Domain.Enums;
using Meal.Infrastructure.DTOs.FoodDTOs;

namespace Meal.Infrastructure.DTOs.MealDTOs
{
    public class MealDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public MealType MealType { get; set; }
        public int TotalCalorie { get; set; }

        public ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}
