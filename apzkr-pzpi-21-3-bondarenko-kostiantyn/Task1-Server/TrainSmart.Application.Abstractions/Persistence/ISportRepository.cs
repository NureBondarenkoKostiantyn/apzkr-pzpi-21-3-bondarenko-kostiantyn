using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.Abstractions.Persistence;

public interface ISportRepository: IGenericRepository<Sport>
{
    Task<Sport?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}