using Dashboard.Aggregator.Models.Enums;

namespace Dashboard.Aggregator.Models.MealModels
{
    public class FoodModel
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

        public ICollection<MealModel> Meals { get; set; } = new List<MealModel>();
    }
}
