using Microsoft.EntityFrameworkCore;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Persistence.Context;

namespace TrainSmart.Persistence.Repositories;

public class UserRepository: GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = true, 
        CancellationToken cancellationToken = default)
    {
        return asNoTracking
            ? await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            : await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}