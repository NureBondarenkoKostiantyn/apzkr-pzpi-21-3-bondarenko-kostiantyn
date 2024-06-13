namespace TrainSmart.Common.DTOs.Session;

public class SessionDto
{
    public Guid Id { get; init; }
    public Guid TeamId { get; init; }
    public int Duration { get; set; }
    public DateTime Date { get; init; }
    public DateTime EndDate { get; init; }
}