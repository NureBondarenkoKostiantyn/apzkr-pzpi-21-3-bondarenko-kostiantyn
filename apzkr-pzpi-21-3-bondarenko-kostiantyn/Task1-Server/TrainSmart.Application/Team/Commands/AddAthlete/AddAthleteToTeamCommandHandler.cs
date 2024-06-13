using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.Team.Commands.AddAthlete;

public class AddAthleteToTeamCommandHandler: IRequestHandler<AddAthleteToTeamCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddAthleteToTeamCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        AddAthleteToTeamCommand request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.TeamId, false, cancellationToken);
        if (team is null)
        {
            throw new AggregateException("Team was not found");
        }

        if (team.Athletes.Any(x => x.AthleteId == request.AthleteId))
        {
            throw new ApplicationException("Athlete is already in this team");
        }

        var athlete = await _unitOfWork
            .GetRepository<IAthleteRepository>()
            .GetByIdAsync(request.AthleteId, false, cancellationToken);
        if (athlete is null)
        {
            throw new ApplicationException("Athlete was not found");
        }

        var teamAthlete = new TeamAthlete(team.Id, athlete.Id);

        team.AddAthlete(teamAthlete);

        _unitOfWork
            .GetRepository<ITeamRepository>()
            .Update(team);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}