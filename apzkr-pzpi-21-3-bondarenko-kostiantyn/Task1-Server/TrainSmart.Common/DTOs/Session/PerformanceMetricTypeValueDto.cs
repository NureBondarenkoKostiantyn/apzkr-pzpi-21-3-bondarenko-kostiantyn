using TrainSmart.Domain.Enums;

namespace TrainSmart.Common.DTOs.Session;

public record PerformanceMetricTypeValueDto(
    PerformanceMetricType MetricType,
    decimal MetricValue);