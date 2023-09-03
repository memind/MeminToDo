using Workout.Domain.Entities.Common;

namespace Application.Repositories
{
    public interface IWriteRepository<T> where T : BaseEntity, new()
    {
        T Create(T entity);
        Task<T> CreateAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        void DeleteById(string id);
        Task DeleteByIdAsync(string id);
    }
}
