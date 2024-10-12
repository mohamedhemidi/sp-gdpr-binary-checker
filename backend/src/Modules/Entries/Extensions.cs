

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Entries.Data;
using Entries.Services;
namespace Entries
{
    public static class Extensions
    {
        public static IServiceCollection AddEntriesModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IBinaryChecker, BinaryCheckerService>();
            return services;
        }

        public static IApplicationBuilder UseEntriesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
