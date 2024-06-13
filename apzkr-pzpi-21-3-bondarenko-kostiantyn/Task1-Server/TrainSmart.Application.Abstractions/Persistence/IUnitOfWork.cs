namespace TrainSmart.Application.Abstractions.Persistence;

public interface IUnitOfWork: IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    T GetRepository<T>() where T : class;
}