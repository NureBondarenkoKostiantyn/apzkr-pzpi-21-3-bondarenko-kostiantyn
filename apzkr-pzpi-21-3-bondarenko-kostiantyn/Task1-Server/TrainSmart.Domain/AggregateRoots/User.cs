using Microsoft.AspNetCore.Identity;

namespace TrainSmart.Domain.AggregateRoots;

public class User: IdentityUser<Guid>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Guid? AthleteId { get; private set; }
}