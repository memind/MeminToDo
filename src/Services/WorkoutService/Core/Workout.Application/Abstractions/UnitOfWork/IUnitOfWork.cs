using Application.Repositories;
using Workout.Domain.Entities.Common;

namespace Workout.Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity, new();
        Task<int> SaveAsync();
        int Save();
    }
}
