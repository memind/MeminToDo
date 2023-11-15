using Budget.Application.Repositories;
using Budget.Domain.Entities.Common;
using Budget.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Budget.Persistance.Concretes.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
    {
        private readonly BudgetDbContext _ctx;

        public WriteRepository(BudgetDbContext dbContext) => this._ctx = dbContext;
        
        private DbSet<T> table { get => _ctx.Set<T>(); }

        public void Create(T entity)
        {
            using (IDbContextTransaction transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    table.Add(entity);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task CreateAsync(T entity)
        {
            using (IDbContextTransaction transaction = await _ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    await table.AddAsync(entity);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public void Delete(Guid id)
        {
            using (IDbContextTransaction transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    T? entity = GetByIdAsTracking(table, id);

                    if (entity is not null)
                        table.Remove(entity);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (IDbContextTransaction transaction = await _ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    T? entity = await GetByIdAsTrackingAsync(table, id);

                    if (entity is not null)
                        await Task.Run(() => table.Remove(entity));

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public T Update(T entity)
        {
            using (IDbContextTransaction transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    table.Update(entity);

                    transaction.Commit();

                    return entity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            using (IDbContextTransaction transaction = await _ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    await Task.Run(() => table.Update(entity));

                    await transaction.CommitAsync();

                    return entity;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        private async Task<T?> GetByIdAsTrackingAsync(DbSet<T> set, Guid id)
        {
            return (await table.AsTracking<T>()
                               .Where(x => x.Id == id)
                               .ToListAsync())
                               .FirstOrDefault();

        }

        private T? GetByIdAsTracking(DbSet<T> set, Guid id)
        {
            return set.AsTracking<T>()
                      .Where(x => x.Id == id)
                      .ToList()
                      .FirstOrDefault();
        }
    }
}
