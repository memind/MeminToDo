﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Skill.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
