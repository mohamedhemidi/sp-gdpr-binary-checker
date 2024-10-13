

using System.Reflection;
using Common.Middlewares;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
        {

            return services;
        }

        public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

        public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumers(assemblies);
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingInMemory((context, config) =>
                {
                    config.ConfigureEndpoints(context);
                });
            });
            return services;
        }

    }
}
