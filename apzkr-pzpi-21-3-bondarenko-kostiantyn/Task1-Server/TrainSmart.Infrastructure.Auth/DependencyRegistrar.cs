using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TrainSmart.Application.Abstractions.Infrastructure.Auth;
using TrainSmart.Infrastructure.Auth.Services;

namespace TrainSmart.Infrastructure.Auth;

public static class DependencyRegistrar
{
    public static void ConfigureInfrastructureAuthDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureServices();
        services.ConfigureAuth(configuration);
    }

    private static void ConfigureServices(
        this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
    }
    
    private static void ConfigureAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthorization();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration.GetSection("JwtOptions")["Issuer"],
                    ValidAudience = configuration.GetSection("JwtOptions")["Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtOptions")["Key"]!))
                };
            });
    }
}