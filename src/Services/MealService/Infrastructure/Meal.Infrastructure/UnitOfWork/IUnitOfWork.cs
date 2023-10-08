using Meal.Domain.Common;
using Meal.Infrastructure.Repositories.Abstract;

namespace Meal.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity;
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;
        Task<int> SaveAsync();
        int Save();
    }
}
