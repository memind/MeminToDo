using Meal.Domain.Common;
using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Meal.Infrastructure.Repositories.Concrete;
using System;

namespace Meal.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealDbContext _ctx;

        public UnitOfWork(MealDbContext ctx)
        {
            this._ctx = ctx;
        }

        public void Dispose() => _ctx.Dispose();

        public async ValueTask DisposeAsync() => await _ctx.DisposeAsync();

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_ctx);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_ctx);

        public int Save() => _ctx.SaveChanges();

        public async Task<int> SaveAsync() => await _ctx.SaveChangesAsync();
    }
}
