using MediatR;
using TrainSmart.Domain.Enums;

namespace TrainSmart.Application.Session.Commands.CreatePerformanceMetric;

public record CreatePerformanceMetricCommand(
    Guid SessionId,
    Guid TeamAthleteId,
    PerformanceMetricType MetricType,
    decimal MetricValue): IRequest<Unit>;