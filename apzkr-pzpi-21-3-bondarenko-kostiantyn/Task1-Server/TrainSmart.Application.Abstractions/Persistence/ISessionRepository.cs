using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Application.Abstractions.Persistence;

public interface ISessionRepository: IGenericRepository<Session>
{
    Task<Session?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<Session?> GetByIdWithMetricsAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<Session>> GetByTeamId(Guid teamId, bool asNoTracking, CancellationToken cancellationToken = default);
}