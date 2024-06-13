using Microsoft.EntityFrameworkCore;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Persistence.Context;

namespace TrainSmart.Persistence.Repositories;

public class TeamRepository: GenericRepository<Team>, ITeamRepository
{
    public TeamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Team?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return asNoTracking
            ? await DbSet.AsNoTracking().Include(x => x.Athletes)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            : await DbSet.Include(x => x.Athletes)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}