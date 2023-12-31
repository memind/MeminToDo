﻿using Meal.Domain.Common;
using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Meal.Infrastructure.Repositories.Concrete
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly MealDbContext _ctx;

        public ReadRepository(MealDbContext dbContext)
        {
            this._ctx = dbContext;
        }

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

        public List<T> GetHistoryAll()
        {
            return Table.TemporalAll().ToList();
        }

        public async Task<List<T>> GetHistoryAllAsync()
        {
            return await Table.TemporalAll().ToListAsync();
        }

        public List<T> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo)
        {
            return Table.TemporalFromTo(utcFrom, utcTo).ToList();
        }

        public async Task<List<T>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo)
        {
            return await Table.TemporalFromTo(utcFrom, utcTo).ToListAsync();
        }
    }
}
