using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Common.Requests.Session;

public class PopulatePerformanceMetricsRequest
{
    public Guid TeamAthleteId { get; set; }
    public List<PerformanceMetricTypeValueDto> Metrics { get; set; }
}