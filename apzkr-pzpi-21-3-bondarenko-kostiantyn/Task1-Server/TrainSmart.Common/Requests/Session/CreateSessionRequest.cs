namespace TrainSmart.Common.Requests.Session;

public class CreateSessionRequest
{
    public Guid TeamId { get; set; }
    public int Duration { get; set; }
}