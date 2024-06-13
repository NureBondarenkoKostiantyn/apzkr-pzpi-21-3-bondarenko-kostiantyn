using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.Session.Commands.CreateHealthMetric;

public class CreateHealthMetricCommandHandler: IRequestHandler<CreateHealthMetricCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateHealthMetricCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        CreateHealthMetricCommand request, 
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

        var healthMetric = new HealthMetric(
            session.Id,
            teamAthlete.Id,
            request.MetricType,
            request.MetricValue);

        session.AddHealthMetric(healthMetric);

        _unitOfWork
            .GetRepository<ISessionRepository>()
            .Update(session);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}