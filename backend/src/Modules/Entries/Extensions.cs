

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entries
{
    public static class Extensions
    {
        public static IServiceCollection AddEntriesModule(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }

        public static IApplicationBuilder UseEntriesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
