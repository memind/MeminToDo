using Budget.Application.Repositories;
using Budget.Domain.Entities.Common;

namespace Budget.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity, new();

        Task<int> SaveAsync();
        int Save();
    }
}
