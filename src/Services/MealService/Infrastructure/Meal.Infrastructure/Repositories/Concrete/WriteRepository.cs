using Meal.Domain.Common;
using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            table.Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public void HardDelete(T entity)
        {
            table.Remove(entity);
        }

        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(() => table.Remove(entity));
        }

        public void SoftDelete(T entity)
        {
            table.Update(entity);
        }

        public async Task SoftDeleteAsync(T entity)
        {
            await Task.Run(() => table.Update(entity));
        }

        public T Update(T entity)
        {
            table.Update(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => table.Update(entity));
            return entity;
        }
    }
}
