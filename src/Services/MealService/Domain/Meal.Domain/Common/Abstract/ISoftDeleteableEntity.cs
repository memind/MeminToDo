namespace Meal.Domain.Common.Abstract
{
    public interface ISoftDeleteableEntity
    {
        public DateTime? DeletedDate { get; set; }
    }
}
