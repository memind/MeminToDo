
using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Meal.Infrastructure.Repositories.Concrete;
using uow = Meal.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Meal.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MealDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MsSqlDatabaseConnectionString")));

            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(uow.IUnitOfWork), typeof(uow.UnitOfWork));

            return services;
        }
    }
}
