using Meal.Domain.Common;
using Meal.Domain.Enums;

namespace Meal.Domain.Entities
{
    public class Meal : BaseEntity
    {
        public Meal() 
        {
            Foods = new List<Food>();
        }

        public MealType MealType { get; set; }
        public int TotalCalorie { get; set; }

        public ICollection<Food> Foods { get; set; }
    }
}
