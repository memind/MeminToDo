using Meal.Domain.Enums;

namespace Meal.Domain.Common.Abstract
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }
    }
}
