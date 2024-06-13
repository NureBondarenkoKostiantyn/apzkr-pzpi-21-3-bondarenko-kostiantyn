using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Domain.Entities;
using TrainSmart.Persistence.Context;
using TrainSmart.Persistence.Repositories;
using TrainSmart.Persistence.UoW;

namespace TrainSmart.Persistence;

public static class DependencyRegistrar
{
    public static void ConfigurePersistenceLayerDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
        services.ConfigureUnitOfWork();
        services.ConfigureIdentity();
    }
    
    private static void ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });
    }

    private static void ConfigureRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ISportRepository, SportRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IAthleteRepository, AthleteRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
    }

    private static void ConfigureUnitOfWork(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    private static void ConfigureIdentity(
        this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(opt => 
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
    }
}