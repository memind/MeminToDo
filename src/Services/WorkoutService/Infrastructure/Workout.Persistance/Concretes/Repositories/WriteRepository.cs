using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities.Common;
using Workout.Persistance.Context;

namespace Workout.Persistance.Concretes.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
    {
        private readonly WorkoutDbContext _context;

        public WriteRepository(WorkoutDbContext dbContext)
        {
            _context = dbContext;
        }

        private DbSet<T> _table { get => _context.Set<T>(); }

        public T Create(T entity)
        {
            _table.Add(entity);
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
            return entity;
        }

        public void DeleteById(string id)
        {
            var entity = _table.Find(id);
            _table.Remove(entity);
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _table.FindAsync(id);
            await Task.Run(() => _table.Remove(entity));
        }

        public T Update(T entity)
        {
            _table.Update(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => _table.Update(entity));
            return entity;
        }
    }
}
