using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Persistence.Context;

public sealed class AppDbContext: IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public DbSet<Sport> Sports { get; private set; }
    public DbSet<Athlete> Athletes { get; private set; }
    public DbSet<Team> Teams { get; private set; }
    public DbSet<TeamAthlete> TeamAthletes { get; private set; }
    public DbSet<Session> Sessions { get; private set; }
    public DbSet<PerformanceMetric> PerformanceMetrics { get; private set; }
    public DbSet<HealthMetric> HealthMetrics { get; private set; }
}