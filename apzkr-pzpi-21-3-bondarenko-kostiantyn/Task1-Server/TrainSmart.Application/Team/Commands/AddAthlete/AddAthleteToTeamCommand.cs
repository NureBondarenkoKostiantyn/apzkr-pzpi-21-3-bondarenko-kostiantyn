using MediatR;

namespace TrainSmart.Application.Team.Commands.AddAthlete;

public record AddAthleteToTeamCommand(
    Guid TeamId,
    Guid AthleteId): IRequest<Unit>;