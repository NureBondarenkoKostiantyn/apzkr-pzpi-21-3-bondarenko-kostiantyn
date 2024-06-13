using Microsoft.Extensions.DependencyInjection;
using TrainSmart.Application.Sport.Queries.GetSports;

namespace TrainSmart.Application;

public static class DependencyRegistrar
{
    public static void ConfigureApplicationLevelDependencies(
        this IServiceCollection services)
    {
        services.ConfigureMediatr();
    }

    private static void ConfigureMediatr(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSportsQueryHandler).Assembly));
    }
}