using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Users.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // services.AddMediatR(typeof(DependencyInjection).Assembly);
            // Version 12:
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Extensions).Assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Extensions).Assembly);

            return services;
        }
    }
}
