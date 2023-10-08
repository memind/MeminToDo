using Meal.Domain.Common.Abstract;
using Meal.Domain.Enums;

namespace Meal.Domain.Common
{
    public class BaseEntity : IEntity, ICreateableEntity, IUpdateableEntity, ISoftDeleteableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
