using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities.Common;
using Workout.Persistance.Context;

namespace Workout.Persistance.Concretes.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly WorkoutDbContext _context;

        public ReadRepository(WorkoutDbContext dbContext)
        {
            _context = dbContext;
        }

        private DbSet<T> _table { get => _context.Set<T>(); }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public T? GetById(string id)
        {
            return _table.Find(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _table.FindAsync(id);
        }

        public List<T> GetUsersAll(string userId)
        {
            return _table.Where(x => x.UserId == Guid.Parse(userId)).ToList();
        }

        public async Task<List<T>> GetUsersAllAsync(string userId)
        {
            return await _table.Where(x => x.UserId == Guid.Parse(userId)).ToListAsync();
        }
    }
}
