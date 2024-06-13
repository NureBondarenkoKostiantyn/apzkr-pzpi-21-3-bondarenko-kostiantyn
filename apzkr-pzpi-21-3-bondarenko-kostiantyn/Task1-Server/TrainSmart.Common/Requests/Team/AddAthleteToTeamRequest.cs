namespace TrainSmart.Common.Requests.Team;

public record AddAthleteToTeamRequest
{
    public Guid AthleteId { get; set; }
};