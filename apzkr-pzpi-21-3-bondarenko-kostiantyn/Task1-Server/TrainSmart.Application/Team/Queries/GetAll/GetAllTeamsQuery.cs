using MediatR;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Queries.GetAll;

public record GetAllTeamsQuery: IRequest<List<TeamDto>>;