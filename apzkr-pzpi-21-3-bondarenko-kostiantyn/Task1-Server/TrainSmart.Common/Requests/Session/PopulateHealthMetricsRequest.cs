using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Common.Requests.Session;

public class PopulateHealthMetricsRequest
{
    public Guid TeamAthleteId { get; set; }
    public List<HealthMetricTypeValueDto> Metrics { get; set; }
}