using Budget.Application.Repositories;
using Budget.Application.UnitOfWork;
using Budget.Domain.Entities.Common;
using Budget.Persistance.Concretes.Repositories;
using Budget.Persistance.Context;

namespace Budget.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BudgetDbContext _ctx;

        public UnitOfWork(BudgetDbContext ctx) => this._ctx = ctx;
        

        public void Dispose() => _ctx.Dispose();

        public async ValueTask DisposeAsync() => await _ctx.DisposeAsync();

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_ctx);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_ctx);
        public int Save() => _ctx.SaveChanges();

        public async Task<int> SaveAsync() => await _ctx.SaveChangesAsync();
    }
}
