using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.PerformanceMetric;

namespace TrainSmart.Application.Session.Queries.GetPerformanceMetrics;

public class GetPerformanceMetricsQueryHandler: IRequestHandler<GetPerformanceMetricsQuery, List<PerformanceMetricDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPerformanceMetricsQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PerformanceMetricDto>> Handle(
        GetPerformanceMetricsQuery request, 
        CancellationToken cancellationToken)
    {
        var session = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByIdWithMetricsAsync(request.SessionId, true, cancellationToken);
        if (session is null)
        {
            throw new ApplicationException("Session was not found");
        }
        
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(session.TeamId, cancellationToken: cancellationToken);
        if (team is null)
        {
            throw new ApplicationException("Team was not found");
        }

        var teamAthlete = team.Athletes.FirstOrDefault(x => x.AthleteId == request.AthleteId);
        if (teamAthlete is null)
        {
            throw new ApplicationException("Athlete was not found");
        }

        var performanceMetrics = session.PerformanceMetrics
            .Where(x => x.TeamAthleteId == teamAthlete.Id && x.MetricType == request.MetricType);

        return _mapper.Map<List<PerformanceMetricDto>>(performanceMetrics);
    }
}