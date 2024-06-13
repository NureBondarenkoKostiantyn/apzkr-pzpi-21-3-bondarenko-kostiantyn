using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.Requests.Session;

public class CreatePerformanceMetricRequest
{
    public Guid TeamAthleteId { get; set; }
    public PerformanceMetricType MetricType { get; set; }
    public decimal MetricValue { get; set; }
}