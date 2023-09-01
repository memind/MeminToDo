using Skill.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Skill.Application.Repositories
{
    public interface IReadRepository<T> where T : SkillBase, new()
    {
        GetManyResult<T> GetAll();
        Task<GetManyResult<T>> GetAllAsync();

        GetManyResult<T> GetFiltered(Expression<Func<T, bool>> filter);
        Task<GetManyResult<T>> GetFilteredAsync(Expression<Func<T, bool>> filter);

        GetOneResult<T> GetById(string id, string type = "object");
        Task<GetOneResult<T>> GetByIdAsync(string id, string type = "object");
    }
}
