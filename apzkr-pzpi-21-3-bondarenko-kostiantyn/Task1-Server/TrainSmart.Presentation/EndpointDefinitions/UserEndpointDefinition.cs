using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrainSmart.Application.User.Commands.Delete;
using TrainSmart.Application.User.Queries.Get;
using TrainSmart.Application.User.Queries.GetById;
using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.EndpointDefinitions;

public class UserEndpointDefinition: IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var userGroup = app.MapGroup("/api/users");

        userGroup.MapGet("/", GetAllUsers);
        userGroup.MapGet("/{id}", GetUserById);
        userGroup.MapDelete("/{id}", DeleteUser);
    }

    private static async Task<IResult> GetAllUsers(
        IMediator mediator)
    {
        var users = await mediator.Send(new GetAllUsersQuery());
        return Results.Ok(users);
    }

    private static async Task<IResult> GetUserById(
        IMediator mediator,
        [FromRoute] Guid id)
    {
        var user = await mediator.Send(new GetUserByIdQuery(id));
        return Results.Ok(user);
    }

    private static async Task<IResult> DeleteUser(
        IMediator mediator,
        [FromRoute] Guid id)
    {
        await mediator.Send(new DeleteUserCommand(id));
        return Results.NoContent();
    }
}