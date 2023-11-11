using User.Domain.Entities;

namespace User.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByUserNameAsync(string userName);
        Task<AppUser?> GetByIdAsync(Guid id);
        Task<bool> ValidateAsync(string username, string password);
    }
}
