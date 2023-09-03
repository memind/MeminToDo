using Workout.Domain.Entities.Common;

namespace Application.Repositories
{
    public interface IReadRepository<T> where T : BaseEntity, new()
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        List<T> GetUsersAll(string id);
        Task<List<T>> GetUsersAllAsync(string id);

        T? GetById(string id);
        Task<T?> GetByIdAsync(string id);
    }
}
