using Meal.Domain.Common;
using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
            table.Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public void HardDelete(Guid id)
        {
            T? entity = table.Find(id);
            if (entity is not null)
                table.Remove(entity);
        }

        public async Task HardDeleteAsync(Guid id)
        {

            T? entity = await table.FindAsync(id);
            if (entity is not null)
                await Task.Run(() => table.Remove(entity));
        }

        public void SoftDelete(T model)
        {
            T? entity = table.Find(model.Id);

            if (entity is not null)
            {
                entity.Status = Domain.Enums.Status.Deleted;
                entity.DeletedDate = DateTime.Now;
            }

            table.Update(entity);
        }

        public async Task SoftDeleteAsync(T model)
        {
            T? entity = table.Find(model.Id);

            if (entity is not null)
            {
                entity.Status = Domain.Enums.Status.Deleted;
                entity.DeletedDate = DateTime.Now;
            }

            table.Update(entity);
        }

        public T Update(T entity)
        {
            entity.CreatedDate = table.First(x => x.Id == entity.Id).CreatedDate;
            table.Update(entity);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.CreatedDate = (await table.FirstAsync(x => x.Id == entity.Id)).CreatedDate;
            await Task.Run(() => table.Update(entity));

            return entity;
        }
    }
}
