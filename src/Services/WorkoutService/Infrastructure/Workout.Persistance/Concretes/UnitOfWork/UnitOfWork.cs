using Application.Repositories;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Domain.Entities.Common;
using Workout.Persistance.Concretes.Repositories;
using Workout.Persistance.Concretes.Repositories.WorkoutRepositories;
using Workout.Persistance.Context;

namespace Workout.Persistance.Concretes.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkoutDbContext _ctx;

        public UnitOfWork(WorkoutDbContext ctx)
        {
            _ctx = ctx;
        }

        public async ValueTask DisposeAsync()
        {
            await _ctx.DisposeAsync();
        }

        public IReadRepository<T> GetReadRepository<T>() where T : BaseEntity, new()
        {
            return new ReadRepository<T>(_ctx);
        }

        public IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity, new()
        {
            return new WriteRepository<T>(_ctx);
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _ctx.SaveChangesAsync();
        }
    }
}
