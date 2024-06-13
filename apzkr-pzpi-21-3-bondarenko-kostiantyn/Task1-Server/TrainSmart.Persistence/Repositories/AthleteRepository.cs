using Microsoft.EntityFrameworkCore;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Persistence.Context;

namespace TrainSmart.Persistence.Repositories;

public class AthleteRepository: GenericRepository<Athlete>, IAthleteRepository
{
    public AthleteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Athlete>> GetByIdsAsync(
        IEnumerable<Guid> ids, 
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Athlete?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = true, 
        CancellationToken cancellationToken = default)
    {
        return asNoTracking
            ? await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            : await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}