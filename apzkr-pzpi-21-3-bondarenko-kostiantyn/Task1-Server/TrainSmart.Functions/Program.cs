using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainSmart.Functions.Services;
using TrainSmart.Functions.Services.Interfaces;
using TrainSmart.Persistence;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IApiService, ApiService>();
        services.ConfigurePersistenceLayerDependencies(context.Configuration);
    })
    .Build();

host.Run();