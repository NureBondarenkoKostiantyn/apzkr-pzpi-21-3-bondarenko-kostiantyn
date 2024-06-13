using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TrainSmart.Application.Session.Commands.Create;
using TrainSmart.Application.Session.Commands.CreateHealthMetric;
using TrainSmart.Application.Session.Commands.CreatePerformanceMetric;
using TrainSmart.Application.Session.Commands.Delete;
using TrainSmart.Application.Session.Commands.PopulateHealthMetrics;
using TrainSmart.Application.Session.Commands.PopulatePerformanceMetrics;
using TrainSmart.Application.Session.Queries.GetHealthMetrics;
using TrainSmart.Application.Session.Queries.GetPerformanceMetrics;
using TrainSmart.Application.Session.Queries.GetSessionById;
using TrainSmart.Application.Session.Queries.GetSessions;
using TrainSmart.Common.Requests.Session;
using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.EndpointDefinitions;

public class SessionEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var sessionGroup = app.MapGroup("/api/sessions");

        sessionGroup.MapGet("/", GetSessions);
        sessionGroup.MapGet("/{id:guid}", GetSession);
        sessionGroup.MapGet("/{id:guid}/performanceMetrics", GetPerformanceMetrics);
        sessionGroup.MapGet("/{id:guid}/healthMetrics", GetHealthMetrics);
        sessionGroup.MapPost("/", CreateSession);
        sessionGroup.MapPost("/{id:guid}/performanceMetrics", CreatePerformanceMetric);
        sessionGroup.MapPost("/{id:guid}/performanceMetrics/batch", PopulatePerformanceMetrics);
        sessionGroup.MapPost("/{id:guid}/healthMetrics", CreateHealthMetric);
        sessionGroup.MapPost("/{id:guid}/healthMetrics/batch", PopulateHealthMetrics);
        sessionGroup.MapDelete("/{id:guid}", DeleteSession);
    }

    private static async Task<IResult> GetSessions(
        IMediator mediator,
        [BindRequired] Guid teamId)
    {
        var sessions = await mediator.Send(new GetSessionsQuery(teamId));
        return Results.Ok(sessions);
    }

    private static async Task<IResult> GetSession(
        IMediator mediator,
        [FromRoute] Guid id)
    {
        var session = await mediator.Send(new GetSessionByIdQuery(id));
        return Results.Ok(session);
    }
    
    private static async Task<IResult> GetPerformanceMetrics(
        IMediator mediator,
        [FromRoute] Guid id,
        [AsParameters] GetPerformanceMetricsRequest getPerformanceMetricsRequest)
    {
        var performanceMetrics = await mediator.Send(new GetPerformanceMetricsQuery(
            id,
            getPerformanceMetricsRequest.AthleteId,
            getPerformanceMetricsRequest.MetricType,
            getPerformanceMetricsRequest.DateFrom,
            getPerformanceMetricsRequest.DateTo));
        return Results.Ok(performanceMetrics);
    }

    private static async Task<IResult> GetHealthMetrics(
        IMediator mediator,
        [FromRoute] Guid id,
        [AsParameters] GetHealthMetricsRequest getHealthMetricsRequest)
    {
        var healthMetrics = await mediator.Send(new GetHealthMetricsQuery(
            id,
            getHealthMetricsRequest.AthleteId,
            getHealthMetricsRequest.MetricType,
            getHealthMetricsRequest.DateFrom,
            getHealthMetricsRequest.DateTo));
        return Results.Ok(healthMetrics);
    }
    
    private static async Task<IResult> CreateSession(
        IMediator mediator,
        [FromBody] CreateSessionRequest createSessionRequest)
    {
        var session = await mediator.Send(new CreateSessionCommand(
            createSessionRequest.TeamId, 
            createSessionRequest.Duration));
        return Results.Ok(session);
    }

    private static async Task<IResult> CreatePerformanceMetric(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] CreatePerformanceMetricRequest createPerformanceMetricRequest)
    {
        await mediator.Send(new CreatePerformanceMetricCommand(
            id,
            createPerformanceMetricRequest.TeamAthleteId,
            createPerformanceMetricRequest.MetricType,
            createPerformanceMetricRequest.MetricValue));
        return Results.NoContent();
    }

    private static async Task<IResult> PopulatePerformanceMetrics(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] PopulatePerformanceMetricsRequest populatePerformanceMetricsRequest)
    {
        await mediator.Send(new PopulatePerformanceMetricsCommand(
            id,
            populatePerformanceMetricsRequest.TeamAthleteId,
            populatePerformanceMetricsRequest.Metrics));
        return Results.NoContent();
    }

    private static async Task<IResult> CreateHealthMetric(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] CreateHealthMetricRequest createHealthMetricRequest)
    {
        await mediator.Send(new CreateHealthMetricCommand(
            id,
            createHealthMetricRequest.TeamAthleteId,
            createHealthMetricRequest.MetricType,
            createHealthMetricRequest.MetricValue));
        return Results.NoContent();
    }

    private static async Task<IResult> PopulateHealthMetrics(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] PopulateHealthMetricsRequest populateHealthMetricsRequest)
    {
        await mediator.Send(new PopulateHealthMetricsCommand(
            id,
            populateHealthMetricsRequest.TeamAthleteId,
            populateHealthMetricsRequest.Metrics));
        return Results.NoContent();
    }
    
    private static async Task<IResult> DeleteSession(
        IMediator mediator,
        [FromRoute] Guid id)
    {
        await mediator.Send(new DeleteSessionCommand(id));
        return Results.NoContent();
    }
}