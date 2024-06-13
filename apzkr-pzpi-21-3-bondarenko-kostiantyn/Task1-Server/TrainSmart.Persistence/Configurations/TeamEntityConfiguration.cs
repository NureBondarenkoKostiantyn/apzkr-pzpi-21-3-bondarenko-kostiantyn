using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Persistence.Configurations;

public class TeamEntityConfiguration: IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasMany(x => x.Athletes);
    }
}