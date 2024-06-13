using TrainSmart.Domain.Entities;

namespace TrainSmart.Domain.AggregateRoots;

public abstract class BaseAggregateRoot: BaseEntity
{
    protected BaseAggregateRoot(Guid id) : base(id){}
}