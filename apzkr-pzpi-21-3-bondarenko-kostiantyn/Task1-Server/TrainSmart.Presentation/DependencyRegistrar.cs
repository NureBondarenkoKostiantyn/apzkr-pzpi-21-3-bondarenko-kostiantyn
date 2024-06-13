using TrainSmart.Application.MappingProfiles;

namespace TrainSmart.Presentation;

public static class DependencyRegistrar
{
    public static void ConfigurePresentationLayerDependencies(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TeamMappingProfile).Assembly);
        services.ConfigureCors();
    }

    private static void ConfigureCors(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
            });
        });
    }
}