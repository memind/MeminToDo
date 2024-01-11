using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Concrete;
using Meal.Application.Services.Abstract;
using Meal.Application.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Meal.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFoodService), typeof(FoodService));
            services.AddScoped(typeof(IMealService), typeof(MealService));
            services.AddScoped(typeof(IMessageConsumerService), typeof(MessageConsumerService));

            return services;
        }
    }
}
