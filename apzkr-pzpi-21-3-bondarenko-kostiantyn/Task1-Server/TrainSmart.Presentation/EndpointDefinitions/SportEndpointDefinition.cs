using MediatR;
using TrainSmart.Application.Sport.Queries.GetSports;
using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.EndpointDefinitions;

public class SportEndpointDefinition: IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var sportGroup = app.MapGroup("/api/sports");

        sportGroup.MapGet("/", GetAllSports);
    }

    private async Task<IResult> GetAllSports(IMediator mediator)
    {
        var sports = await mediator.Send(new GetSportsQuery());
        return Results.Ok(sports);
    }
}