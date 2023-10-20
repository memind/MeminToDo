using Meal.Domain.Common;
using System.Linq.Expressions;

namespace Meal.Infrastructure.Repositories.Abstract
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        List<T> GetAll(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includeProperties);
        T Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includeProperties);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includeProperties);

        List<T> GetHistoryAll();

        Task<List<T>> GetHistoryAllAsync();

        List<T> GetHistoryFromTo(DateTime utcFrom, DateTime utcTo);

        Task<List<T>> GetHistoryFromToAsync(DateTime utcFrom, DateTime utcTo);
    }
}
