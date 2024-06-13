using TrainSmart.Domain.Enums;

namespace TrainSmart.Domain.Entities;

public class HealthMetric: BaseEntity
{
    public Guid SessionId { get; private set; }
    public Guid TeamAthleteId { get; private set; }
    public HealthMetricType MetricType { get; private set; }
    public decimal MetricValue { get; private set; }
    public DateTime TimeStamp { get; private set; }
    
    public HealthMetric(
        Guid sessionId, 
        Guid teamAthleteId, 
        HealthMetricType metricType, 
        decimal metricValue) : base(Guid.Empty)
    {
        SessionId = sessionId;
        TeamAthleteId = teamAthleteId;
        MetricType = metricType;
        MetricValue = metricValue;
        TimeStamp = DateTime.UtcNow;
    }
}