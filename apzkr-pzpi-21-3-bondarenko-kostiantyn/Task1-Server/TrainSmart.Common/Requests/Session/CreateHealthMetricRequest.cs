using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.Requests.Session;

public class CreateHealthMetricRequest
{
    public Guid TeamAthleteId { get; set; }
    public HealthMetricType MetricType { get; set; }
    public decimal MetricValue { get; set; }
}