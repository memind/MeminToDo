using Meal.Domain.Common;
using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Meal.Infrastructure.Repositories.Concrete
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly MealDbContext _ctx;

        public WriteRepository(MealDbContext dbContext)
        {
            this._ctx = dbContext;
        }

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

        public void HardDelete(Guid id)
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

        public async Task HardDeleteAsync(Guid id)
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

        public void SoftDelete(T model)
        {
            using (IDbContextTransaction transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    T? entity = GetByIdAsTracking(table, model.Id);

                    if (entity is not null)
                    {
                        entity.Status = Domain.Enums.Status.Deleted;
                        entity.DeletedDate = DateTime.Now;

                        table.Update(entity);

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task SoftDeleteAsync(T model)
        {
            using (IDbContextTransaction transaction = await _ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    T? entity = await GetByIdAsTrackingAsync(table, model.Id);

                    if (entity is not null)
                    {
                        entity.Status = Domain.Enums.Status.Deleted;
                        entity.DeletedDate = DateTime.Now;

                        table.Update(entity);
                    }

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
                    entity.CreatedDate = table.First(x => x.Id == entity.Id).CreatedDate;
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
                    entity.CreatedDate = (await table.FirstAsync(x => x.Id == entity.Id)).CreatedDate;
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
