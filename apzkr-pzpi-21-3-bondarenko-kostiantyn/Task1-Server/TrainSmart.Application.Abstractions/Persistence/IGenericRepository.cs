namespace TrainSmart.Application.Abstractions.Persistence;

public interface IGenericRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}