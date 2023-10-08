using Meal.Domain.Common;
using Meal.Domain.Enums;

namespace Meal.Domain.Entities
{
    public class Food : BaseEntity
    {
        public Food()
        {
            Meals = new List<Meal>();
        }
        public required string Name { get; set; }
        public int CalorieByServing { get; set; }
        public FoodCategory Category { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
