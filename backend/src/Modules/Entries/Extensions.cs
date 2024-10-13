

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Entries.Data;
using Entries.Services;
using Entries.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
namespace Entries
{
    public static class Extensions
    {
        public static IServiceCollection AddEntriesModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IBinaryChecker, BinaryCheckerService>();

            // MongoDB Driver:

            // Add MongoDB settings to DI
            services.Configure<MongoSettings>(
                config.GetSection("MongoSettings"));

            // Register MongoDB client
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            // Register your custom service for accessing MongoDB collections
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });
            return services;
        }

        public static IApplicationBuilder UseEntriesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
