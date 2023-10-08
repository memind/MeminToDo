using Meal.Domain.Common;
using Meal.Domain.Enums;

namespace Meal.Domain.Entities
{
    public class Meal : BaseEntity
    {
        public Meal() 
        {
            Foods = new List<MealFood>();
        }

        public MealType MealType { get; set; }
        public int TotalCalorie { get; set; }

        public ICollection<MealFood> Foods { get; set; }
    }
}
