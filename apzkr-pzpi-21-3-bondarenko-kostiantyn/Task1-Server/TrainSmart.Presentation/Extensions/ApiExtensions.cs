using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.Extensions;

public static class ApiExtensions
{
    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && t is {IsAbstract: false, IsInterface: false})
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach(var endpointDef in endpointDefinitions)
        {
            endpointDef.RegisterEndpoints(app);
        }
    }
}