using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Application.Abstractions.Persistence;

public interface ITeamRepository: IGenericRepository<Team>
{
    Task<Team?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}