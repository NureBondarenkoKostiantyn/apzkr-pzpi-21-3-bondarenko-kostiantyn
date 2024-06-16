using TrainSmart.Domain.Entities;

namespace TrainSmart.Domain.AggregateRoots;

public class Session: BaseAggregateRoot
{
    public Guid TeamId { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime EndDate { get; private set; }
    public int Duration { get; private set; }
    
    private readonly List<PerformanceMetric> _performanceMetrics = [];
    private readonly List<HealthMetric> _healthMetrics = [];

    public IReadOnlyCollection<PerformanceMetric> PerformanceMetrics => _performanceMetrics;
    public IReadOnlyCollection<HealthMetric> HealthMetrics => _healthMetrics;
    
    public Session(Guid teamId, int duration) : base(Guid.NewGuid())
    {
        TeamId = teamId;
        Date = DateTime.UtcNow;
        EndDate = Date.AddMinutes(duration);
        Duration = duration;
    }

    public void AddPerformanceMetric(PerformanceMetric performanceMetric)
    {
        if (_performanceMetrics.All(x => x.Id != performanceMetric.Id))
        {
            _performanceMetrics.Add(performanceMetric);
        }
    }

    public void PopulatePerformanceMetrics(IEnumerable<PerformanceMetric> performanceMetrics)
    {
        var validPerformanceMetrics = performanceMetrics
            .Where(x => !_performanceMetrics.Contains(x));
        _performanceMetrics.AddRange(validPerformanceMetrics);
    }

    public void AddHealthMetric(HealthMetric healthMetric)
    {
        if (_healthMetrics.All(x => x.Id != healthMetric.Id))
        {
            _healthMetrics.Add(healthMetric);
        }
    }

    public void PopulateHealthMetrics(IEnumerable<HealthMetric> healthMetrics)
    {
        var validHealthMetrics = healthMetrics
            .Where(x => !_healthMetrics.Contains(x));
        _healthMetrics.AddRange(validHealthMetrics);
    }
}