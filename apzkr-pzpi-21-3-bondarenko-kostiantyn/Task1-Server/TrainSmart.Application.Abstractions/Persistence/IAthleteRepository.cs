using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Application.Abstractions.Persistence;

public interface IAthleteRepository: IGenericRepository<Athlete>
{
    Task<IEnumerable<Athlete>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task<Athlete?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}