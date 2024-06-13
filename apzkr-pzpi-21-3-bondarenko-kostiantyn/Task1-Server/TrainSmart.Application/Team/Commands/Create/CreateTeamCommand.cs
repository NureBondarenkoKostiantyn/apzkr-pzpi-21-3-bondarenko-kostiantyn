using MediatR;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Commands.Create;

public record CreateTeamCommand(
    string Name,
    Guid SportId,
    string? Description,
    string? CountryName): IRequest<TeamDto>;