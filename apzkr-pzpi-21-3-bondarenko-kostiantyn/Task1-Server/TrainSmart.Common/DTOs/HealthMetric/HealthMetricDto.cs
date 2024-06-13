using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.DTOs.HealthMetric;

public class HealthMetricDto
{
    public Guid SessionId { get; set; }
    public Guid TeamAthleteId { get; set; }
    public HealthMetricType MetricType { get; set; }
    public double MetricValue { get; set; }
    public DateTime TimeStamp { get; set; }
}