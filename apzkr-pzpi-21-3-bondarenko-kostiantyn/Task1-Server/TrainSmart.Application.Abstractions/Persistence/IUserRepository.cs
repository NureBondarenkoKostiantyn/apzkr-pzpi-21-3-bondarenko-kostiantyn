using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Application.Abstractions.Persistence;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}