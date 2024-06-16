using System.Net;
using System.Text;
using Bogus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Session;
using TrainSmart.Common.Requests.Session;
using TrainSmart.Domain.Enums;
using TrainSmart.Functions.Requests;
using TrainSmart.Functions.Services.Interfaces;

namespace TrainSmart.Functions.Functions;

public class PopulateSessionMetricsFunction
{
    private readonly IApiService _apiService;
    private readonly IUnitOfWork _unitOfWork;

    public PopulateSessionMetricsFunction(
        IApiService apiService, 
        IUnitOfWork unitOfWork)
    {
        _apiService = apiService;
        _unitOfWork = unitOfWork;
    }

    [Function(nameof(PopulateSessionMetricsFunction))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var requestDto = JsonConvert.DeserializeObject<StartPopulateSessionMetricsFunctionDto>(
            Encoding.UTF8.GetString(ReadFully(req.Body)));
        if (requestDto is null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var session = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByIdAsync(requestDto.SessionId);
        if (session is null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(session.TeamId);
        if (team is null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var faker = new Faker();
        var random = new Random();

        foreach (var teamAthlete in team.Athletes)
        {
            var count = random.Next(50, 200);
            var performanceMetrics = new List<PerformanceMetricTypeValueDto>();
            var healthMetrics = new List<HealthMetricTypeValueDto>();
            for (var i = 0; i < count; i++)
            {
                performanceMetrics.Add(new PerformanceMetricTypeValueDto(
                    faker.PickRandom<PerformanceMetricType>(), 
                    faker.Random.Decimal(0, 100)));
                healthMetrics.Add(new HealthMetricTypeValueDto(
                    faker.PickRandom<HealthMetricType>(), 
                    faker.Random.Decimal(60, 120)));
            }
        
            await PopulatePerformanceMetrics(requestDto.SessionId, teamAthlete.Id, performanceMetrics);
            await PopulateHealthMetrics(requestDto.SessionId, teamAthlete.Id, healthMetrics);
        }

        var response = req.CreateResponse(HttpStatusCode.NoContent);

        return response;
        
    }
    
    private async Task PopulatePerformanceMetrics(
        Guid sessionId,
        Guid teamAthleteId,
        List<PerformanceMetricTypeValueDto> performanceMetrics)
    {
        var populatePerformanceMetricsRequest = new PopulatePerformanceMetricsRequest
        {
            TeamAthleteId = teamAthleteId,
            Metrics = performanceMetrics
        };
        var response = await _apiService.PostAsync(
            $"https://localhost:7280/api/sessions/{sessionId}/performanceMetrics/batch", 
            populatePerformanceMetricsRequest);

        response.EnsureSuccessStatusCode();
    }
    
    private async Task PopulateHealthMetrics(
        Guid sessionId,
        Guid teamAthleteId,
        List<HealthMetricTypeValueDto> healthMetrics)
    {
        var populatePerformanceMetricsRequest = new PopulateHealthMetricsRequest
        {
            TeamAthleteId = teamAthleteId,
            Metrics = healthMetrics
        };
        var response = await _apiService.PostAsync(
            $"https://localhost:7280/api/sessions/{sessionId}/healthMetrics/batch", 
            populatePerformanceMetricsRequest);

        response.EnsureSuccessStatusCode();
    }

    private static byte[] ReadFully(Stream input)
    {
        var buffer = new byte[16*1024];
        using var ms = new MemoryStream();
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }
        return ms.ToArray();
    }
}