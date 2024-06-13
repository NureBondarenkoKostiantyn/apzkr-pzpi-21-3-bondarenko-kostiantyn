using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.Requests.Session;

public class GetPerformanceMetricsRequest
{
    public Guid AthleteId { get; set; }
    public PerformanceMetricType? MetricType { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}