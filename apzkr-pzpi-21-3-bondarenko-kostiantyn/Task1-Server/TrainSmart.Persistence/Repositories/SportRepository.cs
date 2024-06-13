using Microsoft.EntityFrameworkCore;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Persistence.Context;

namespace TrainSmart.Persistence.Repositories;

public class SportRepository: GenericRepository<Sport>, ISportRepository
{
    public SportRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Sport?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = true, 
        CancellationToken cancellationToken = default)
    {
        return asNoTracking
            ? await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            : await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}