using Meal.Domain.Common;

namespace Meal.Infrastructure.Repositories.Abstract
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        Task CreateAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        void SoftDelete(T entity);
        Task SoftDeleteAsync(T entity);

        void HardDelete(T entity);
        Task HardDeleteAsync(T entity);
    }
}
