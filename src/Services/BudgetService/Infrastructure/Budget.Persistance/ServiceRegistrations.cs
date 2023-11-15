using Budget.Application.Repositories;
using Budget.Persistance.Concretes.Repositories;
using uow = Budget.Persistance.UnitOfWork;
using Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Budget.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Budget.Persistance.Context;
using Budget.Persistance.Concretes.Factories;
using Budget.Application.Abstractions.Factories;
using Budget.Application.Abstractions.Services;
using Budget.Persistance.Concretes.Services;

namespace Budget.Persistance
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg, IHostBuilder host)
        {
            services.AddDbContext<BudgetDbContext>(options =>
                options.UseSqlServer(cfg.GetConnectionString("MsSqlDatabaseConnectionString")).EnableSensitiveDataLogging());

            //host.UseSerilog(SeriLogger.Configure);
            //services.AddTransient<LoggingDelegatingHandler>();

            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(IUnitOfWork), typeof(uow.UnitOfWork));

            services.AddScoped(typeof(IMoneyFlowFactory), typeof(MoneyFlowFactory));

            services.AddScoped(typeof(IMoneyFlowService), typeof(MoneyFlowService));
            services.AddScoped(typeof(IBudgetAccountService), typeof(BudgetAccountService));
            services.AddScoped(typeof(IWalletService), typeof(WalletService));

            return services;
        }
    }
}
