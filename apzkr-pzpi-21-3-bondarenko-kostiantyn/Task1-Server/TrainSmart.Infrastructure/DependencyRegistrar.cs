using Microsoft.Extensions.DependencyInjection;
using TrainSmart.Application.Abstractions.Infrastructure.Database;
using TrainSmart.Infrastructure.Services;

namespace TrainSmart.Infrastructure;

public static class DependencyRegistrar
{
    public static void ConfigureInfrastructureLayerDependencies(
        this IServiceCollection services)
    {
        services.ConfigureServices();
    }

    private static void ConfigureServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDatabaseService, DatabaseService>();
    }
}