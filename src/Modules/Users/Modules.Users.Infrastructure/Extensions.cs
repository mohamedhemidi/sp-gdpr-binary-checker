
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Domain.Entities;
using Modules.Users.Infrastructure.Services;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Modules.Users.Application.Common.Interfaces;
using Modules.Users.Application.Common.Services;
using Modules.Users.Domain.Models;
using MongoDB.Driver;
namespace Modules.Users.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {

            // Common Services:

            services.AddHttpClient();

            // Authentication Services:

            services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
            services.AddTransient<IJwtToken, JwtToken>();


            // Identity Service:

            services.ConfigureIdentitySettings(config);
            // DB Services:
            /*var connectionString = config.GetConnectionString("mongodb");*/

            // Configure MongoDB settings
            services.Configure<MongoSettings>(config.GetSection("MongoSettings"));

            // Add MongoDB Identity configuration
            /*services.AddIdentity<AppUser, AppRole>()*/
            /*        .AddMongoDbStores<AppUser, AppRole, Guid>(*/
            /*            config["MongoSettings:ConnectionString"],*/
            /*            config["MongoSettings:DatabaseName"]*/
            /*        )*/
            /*        .AddDefaultTokenProviders();*/

            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

            var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = config["MongoSettings:ConnectionString"],
                    DatabaseName = config["MongoSettings:DatabaseName"]
                }
            };

            services.ConfigureMongoDbIdentity<AppUser, AppRole, Guid>(mongoDbIdentityConfig)
                .AddUserManager<UserManager<AppUser>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddDefaultTokenProviders();



            return services;
        }
    }
}
