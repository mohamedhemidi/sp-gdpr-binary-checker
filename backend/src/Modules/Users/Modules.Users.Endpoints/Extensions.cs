using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Application;
using Modules.Users.Infrastructure;
namespace Modules.Users.Endpoints
{
    public static class Extensions
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddApplication();
            services.AddInfrastructure(config);
            return services;
        }

        public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
