using System.Text.Json.Serialization;
using MediatR;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Commands.Update;

public record UpdateTeamCommand(
    Guid Id,
    string Name,
    string? Description,
    string? CountryName) : IRequest<TeamDto>
{
    [JsonIgnore] 
    public Guid Id { get; set; } = Id;
}