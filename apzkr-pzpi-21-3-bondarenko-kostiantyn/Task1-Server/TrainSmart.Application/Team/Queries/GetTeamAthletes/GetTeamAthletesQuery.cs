using MediatR;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Team.Queries.GetTeamAthletes;

public record GetTeamAthletesQuery(Guid TeamId): IRequest<IEnumerable<AthleteDto>>;