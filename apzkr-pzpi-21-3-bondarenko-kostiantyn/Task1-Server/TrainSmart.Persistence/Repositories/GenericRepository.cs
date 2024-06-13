using Microsoft.EntityFrameworkCore;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Persistence.Context;

namespace TrainSmart.Persistence.Repositories;

public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity: class
{
    private readonly AppDbContext _context;
    protected readonly DbSet<TEntity> DbSet;

    protected GenericRepository(AppDbContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return asNoTracking
            ? await DbSet.AsNoTracking().ToListAsync(cancellationToken)
            : await DbSet.ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }
}