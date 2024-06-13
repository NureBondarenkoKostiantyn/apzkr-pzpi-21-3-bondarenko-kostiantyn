namespace TrainSmart.Domain.AggregateRoots;

public class 
    Sport: BaseAggregateRoot
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    protected Sport(Guid id) : base(id)
    {
    }
}