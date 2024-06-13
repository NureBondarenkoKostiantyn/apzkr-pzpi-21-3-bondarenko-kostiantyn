namespace TrainSmart.Common.DTOs.Athlete;

public class AthleteDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string? Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}