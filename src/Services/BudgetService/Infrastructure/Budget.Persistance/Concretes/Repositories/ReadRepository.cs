using Budget.Application.Repositories;
using Budget.Domain.Entities.Common;
using Budget.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Budget.Persistance.Concretes.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly BudgetDbContext _ctx;

        public ReadRepository(BudgetDbContext dbContext) => this._ctx = dbContext;
        
        private DbSet<T> Table { get => _ctx.Set<T>(); }

        public T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (orderBy is not null)
                orderBy(query).ToList();

            query = query.Where(predicate).AsTracking<T>();

            T? result = query.FirstOrDefault();

            if (result is not null) return result;

            throw new Exception();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return orderBy(query).ToList();

            return query.ToList();
        }

        public List<T> GetAllAsNoTracking(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return orderBy(query.AsNoTracking()).ToList();

            return query.AsNoTracking().ToList();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;


            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsyncAsNoTracking(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;


            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return await orderBy(query.AsNoTracking()).ToListAsync();

            return await query.AsNoTracking().ToListAsync();
        }

        public T GetAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (orderBy is not null)
                orderBy(query).ToList();

            query = query.Where(predicate);

            T? result = query.AsNoTracking().FirstOrDefault();

            if (result is not null) return result;

            throw new Exception();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (orderBy is not null)
                await orderBy(query).ToListAsync();

            query = query.Where(predicate).AsTracking<T>();

            T? result = await query.FirstOrDefaultAsync();

            if (result is not null)
                return result;

            else
                throw new Exception();
        }

        public async Task<T> GetAsyncAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            if (orderBy is not null)
                await orderBy(query).ToListAsync();

            query = query.Where(predicate);

            T? result = await query.AsNoTracking().FirstOrDefaultAsync();

            if (result is not null)
                return result;

            else
                throw new Exception();
        }
    }
}
