using Entertainment.Domain.Entities.Common;

namespace Entertainment.Application.Repositories
{
    public interface IWriteRepository<T> where T : EntertainmentBase, new()
    {
        int Create(T entity);
        Task<int> CreateAsync(T entity);

        int Delete(string id);
        Task<int> DeleteAsync(string id);

        int Update(T entity);
        Task<int> UpdateAsync(T entity);
    }
}
