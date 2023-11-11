using Microsoft.EntityFrameworkCore;
using System;
using User.Application.Abstractions.Repositories;
using User.Domain.Entities;
using User.Persistance.Context;

namespace User.Persistance.Concretes.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetByUserNameAsync(string userName) => 
            await _context.Users.FirstOrDefaultAsync(f => f.Username == userName);

        public async Task<AppUser?> GetByIdAsync(Guid id) => 
            await _context.Users.FirstOrDefaultAsync(f => f.Id == id);

        public async Task<bool> ValidateAsync(string email, string password) => 
            await _context.Users.AnyAsync(f => f.Email == email && f.Password == password);
    }
}
