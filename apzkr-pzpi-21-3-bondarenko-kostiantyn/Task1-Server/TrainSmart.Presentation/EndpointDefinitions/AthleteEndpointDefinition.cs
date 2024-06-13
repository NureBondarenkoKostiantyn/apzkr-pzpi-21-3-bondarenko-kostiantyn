using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrainSmart.Application.Athlete.Commands.BindToUser;
using TrainSmart.Application.Athlete.Queries.Get;
using TrainSmart.Application.Athlete.Queries.GetById;
using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.EndpointDefinitions;

public class AthleteEndpointDefinition: IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var athleteGroup = app.MapGroup("/api/athletes");

        athleteGroup.MapGet("/", GetAllAthletes);
        athleteGroup.MapGet("/{id}", GetAthleteById);
        athleteGroup.MapPost("/", BindAthleteToUser);
    }

    private static async Task<IResult> GetAllAthletes(
        IMediator mediator)
    {
        var athletes = await mediator.Send(new GetAllAthletesQuery());
        return Results.Ok(athletes);
    }
    
    private static async Task<IResult> GetAthleteById(
        IMediator mediator,
        [FromRoute] Guid id)
    {
        var athlete = await mediator.Send(new GetAthleteByIdQuery(id));
        return Results.Ok(athlete);
    }
    
    private async Task<IResult> BindAthleteToUser(
        IMediator mediator,
        [FromBody] BindAthleteToUserCommand bindAthleteToUserCommand)
    {
        var athlete = await mediator.Send(bindAthleteToUserCommand);
        return Results.Ok(athlete);
    }
}