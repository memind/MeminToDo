using Budget.Domain.Entities.Common;

namespace Budget.Application.Repositories
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        Task CreateAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}
