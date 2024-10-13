using Modules.Users.Endpoints;
using Common;
using Entries;
using Entries.ExternalEvents;
using System.Reflection;
namespace App
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSharedFramework(_configuration);
            services.AddUsersModule(_configuration);
            services.AddEntriesModule(_configuration);
            services.AddMassTransitService(_configuration,
                Assembly.GetAssembly(typeof(DeleteEntriesConsumer))!
            );
            services.AddControllers().AddJsonOptions(options =>
            {
                // Prevent Object keys PascalCase formatting
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();



            app.UseAuthentication();
            app.UseAuthorization();


            app.UseSharedFramework();
            app.UseUsersModule();
            app.UseEntriesModule();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.WithOrigins(
                    "http://localhost:4200"
                );
                options.AllowCredentials();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("BinaryChecker API"));
            });

        }
    }
}
