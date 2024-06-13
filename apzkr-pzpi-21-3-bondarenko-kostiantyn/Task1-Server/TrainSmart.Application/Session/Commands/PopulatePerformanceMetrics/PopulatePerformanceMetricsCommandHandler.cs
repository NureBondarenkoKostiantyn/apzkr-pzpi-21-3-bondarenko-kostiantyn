using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.Session.Commands.PopulatePerformanceMetrics;

public class PopulatePerformanceMetricsCommandHandler: IRequestHandler<PopulatePerformanceMetricsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public PopulatePerformanceMetricsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        PopulatePerformanceMetricsCommand request, 
        CancellationToken cancellationToken)
    {
        var session = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByIdAsync(request.SessionId, false, cancellationToken);
        if (session is null)
        {
            throw new ApplicationException("Session was not found");
        }

        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(session.TeamId, false, cancellationToken);

        var teamAthlete = team!.Athletes.FirstOrDefault(x => x.Id == request.TeamAthleteId);
        if (teamAthlete is null)
        {
            throw new ApplicationException("Team athlete was not found");
        }

        var performanceMetrics = new List<PerformanceMetric>();
        foreach (var (metricType, metricValue) in request.Metrics)
        {
            var performanceMetric = new PerformanceMetric(
                session.Id,
                teamAthlete.Id,
                metricType,
                metricValue);
            performanceMetrics.Add(performanceMetric);
        }

        session.PopulatePerformanceMetrics(performanceMetrics);

        _unitOfWork
            .GetRepository<ISessionRepository>()
            .Update(session);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}