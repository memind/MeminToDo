using Dashboard.Aggregator.Models.Enums;

namespace Dashboard.Aggregator.Models.MealModels
{
    public class MealModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public MealType MealType { get; set; }
        public int TotalCalorie { get; set; }

        public ICollection<FoodModel> Foods { get; set; } = new List<FoodModel>();
    }
}
