using MediatR;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Queries.GetById;

public record GetTeamByIdQuery(Guid Id) : IRequest<TeamDto>;