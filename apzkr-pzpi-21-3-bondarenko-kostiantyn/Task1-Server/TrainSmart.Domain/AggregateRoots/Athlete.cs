namespace TrainSmart.Domain.AggregateRoots;

public sealed class Athlete: BaseAggregateRoot
{
    public Guid UserId { get; private set; }

    public Athlete(Guid userId) : base(Guid.NewGuid())
    {
        UserId = userId;
    }
}