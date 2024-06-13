using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.DTOs.Session;

public record HealthMetricTypeValueDto(
    HealthMetricType MetricType,
    decimal MetricValue);