using Microsoft.Extensions.DependencyInjection;
using TrainSmart.Common.Options;
using TrainSmart.Common.Options.Jwt;

namespace TrainSmart.Common;

public static class DependencyRegistrar
{
    public static void ConfigureCommonLayerDependencies(
        this IServiceCollection services)
    {
        services.ConfigureOptions();
    }
    
    private static void ConfigureOptions(
        this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
    }
}