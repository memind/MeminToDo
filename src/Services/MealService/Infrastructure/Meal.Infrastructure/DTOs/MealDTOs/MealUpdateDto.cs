using Meal.Domain.Entities;
using Meal.Domain.Enums;

namespace Meal.Infrastructure.DTOs.MealDTOs
{
    public class MealUpdateDto
    {
        public Guid Id { get; set; }
        public MealType MealType { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
