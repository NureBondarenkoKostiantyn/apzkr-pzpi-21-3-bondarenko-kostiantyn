using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.DTOs.PerformanceMetric;

public class PerformanceMetricDto
{
    public Guid SessionId { get; set; }
    public Guid TeamAthleteId { get; set; }
    public PerformanceMetricType MetricType { get; set; }
    public double MetricValue { get; set; }
    public DateTime TimeStamp { get; set; }
}