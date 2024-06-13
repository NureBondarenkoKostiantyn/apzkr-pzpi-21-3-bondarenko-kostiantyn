using Microsoft.AspNetCore.Mvc;
using TrainSmart.Application.Abstractions.Infrastructure.Auth;
using TrainSmart.Common.DTOs.Auth;
using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.EndpointDefinitions;

public class AuthEndpointDefinition: IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var authGroup = app.MapGroup("/api/auth");

        authGroup.MapPost("/login", Login);
        authGroup.MapPost("/signup", Signup);
    }

    private static async Task<IResult> Login(
        IAuthService authService,
        [FromBody] LoginDto loginDto)
    {
        var jwtTokenDto = await authService.LoginAsync(loginDto);
        return Results.Ok(jwtTokenDto);
    }

    private static async Task<IResult> Signup(
        IAuthService authService,
        [FromBody] SignupDto signupDto)
    {
        await authService.SignupAsync(signupDto);
        return Results.NoContent();
    }
}