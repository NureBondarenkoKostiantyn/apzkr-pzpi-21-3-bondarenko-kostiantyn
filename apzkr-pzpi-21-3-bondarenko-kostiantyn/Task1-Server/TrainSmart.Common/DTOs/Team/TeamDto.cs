namespace TrainSmart.Common.DTOs.Team;

public class TeamDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; set; }
    public string? CountryName { get; init; }
    public Guid SportId { get; init; }
    public string? SportName { get; set; }
}