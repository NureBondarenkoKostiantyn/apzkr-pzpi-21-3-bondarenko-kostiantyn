using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainSmart.Domain.AggregateRoots;

namespace TrainSmart.Persistence.Configurations;

public class AthleteEntityConfiguration: IEntityTypeConfiguration<Athlete>
{
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        
    }
}