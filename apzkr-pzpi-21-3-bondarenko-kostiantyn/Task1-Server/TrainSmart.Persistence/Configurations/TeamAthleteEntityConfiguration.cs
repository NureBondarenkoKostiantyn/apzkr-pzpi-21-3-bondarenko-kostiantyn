using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Persistence.Configurations;

public class TeamAthleteEntityConfiguration: IEntityTypeConfiguration<TeamAthlete>
{
    public void Configure(EntityTypeBuilder<TeamAthlete> builder)
    {
    }
}