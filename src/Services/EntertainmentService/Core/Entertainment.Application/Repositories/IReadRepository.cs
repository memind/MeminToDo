
using Entertainment.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Entertainment.Application.Repositories
{
    public interface IReadRepository<T> where T : EntertainmentBase, new()
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        List<T> GetUsersAll(string userId);
        Task<List<T>> GetUsersAllAsync(string userId);

        T GetById(string id);
        Task<T> GetByIdAsync(string id);
    }
}
