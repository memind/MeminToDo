namespace Meal.Domain.Common.Abstract
{
    public interface IUpdateableEntity
    {
        public DateTime? UpdatedDate { get; set; }
    }
}
