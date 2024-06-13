using MediatR;
using TrainSmart.Application.Abstractions.Persistence;

namespace TrainSmart.Application.Team.Commands.RemoveAthlete;

public class RemoveAthleteFromTeamCommandHandler: IRequestHandler<RemoveAthleteFromTeamCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAthleteFromTeamCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        RemoveAthleteFromTeamCommand request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.TeamId, false, cancellationToken);
        if (team is null)
        {
            throw new AggregateException("Team was not found");
        }

        var teamAthlete = team.Athletes.FirstOrDefault(x => x.AthleteId == request.AthleteId);
        if (teamAthlete is null)
        {
            throw new ApplicationException("Athlete is not in this team");
        }

        team.RemoveAthlete(teamAthlete);

        _unitOfWork
            .GetRepository<ITeamRepository>()
            .Update(team);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}