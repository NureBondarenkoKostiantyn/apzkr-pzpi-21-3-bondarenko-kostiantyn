using MediatR;
using TrainSmart.Common.DTOs.PerformanceMetric;
using TrainSmart.Domain.Enums;

namespace TrainSmart.Application.Session.Queries.GetPerformanceMetrics;

public record GetPerformanceMetricsQuery(
    Guid SessionId,
    Guid AthleteId,
    PerformanceMetricType? MetricType = default,
    DateTime? DateFrom = default,
    DateTime? DateTo = default): IRequest<List<PerformanceMetricDto>>;