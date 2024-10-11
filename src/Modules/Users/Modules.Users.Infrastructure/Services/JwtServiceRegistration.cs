using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Common.Base;

namespace Modules.Users.Infrastructure.Services;

public static class JwtServiceRegistration
{
    public static IServiceCollection ConfigureIdentitySettings(this IServiceCollection services, IConfiguration config)
    {



        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = config["JwtSettings:Audience"],
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]!)
                )
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<Exception>
                {
                    IsSuccess = false,
                    Message = "Unauthorized access. Please provide a valid token.",
                    StatusCode = 401,

                };

                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

                return context.Response.WriteAsync(jsonResponse);
            }
        };
    });
        return services;
    }
}
