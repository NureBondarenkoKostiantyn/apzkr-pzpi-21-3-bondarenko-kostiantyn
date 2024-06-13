namespace TrainSmart.Domain.Entities;

public class TeamAthlete: BaseEntity
{
    public Guid TeamId { get; private set; }
    public Guid AthleteId { get; private set; }
    public DateTime? DateJoined { get; init; }

    public TeamAthlete(Guid teamId, Guid athleteId) : base(Guid.Empty)
    {
        TeamId = teamId;
        AthleteId = athleteId;
        DateJoined = DateTime.UtcNow;;
    }
}