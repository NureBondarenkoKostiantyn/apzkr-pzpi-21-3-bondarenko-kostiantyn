using MediatR;

namespace TrainSmart.Application.Team.Commands.RemoveAthlete;

public record RemoveAthleteFromTeamCommand(
    Guid TeamId,
    Guid AthleteId): IRequest<Unit>;