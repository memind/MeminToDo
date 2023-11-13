using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using User.Domain.Entities;

namespace User.Persistance.Context
{
    public class UserDbContext : IdentityDbContext<AppUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public UserDbContext() : base() { }
        
        public DbSet<AppUser> Users { get; set; }

    }
}
