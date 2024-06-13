using MediatR;
using TrainSmart.Domain.Enums;

namespace TrainSmart.Application.Session.Commands.CreateHealthMetric;

public record CreateHealthMetricCommand(
    Guid SessionId,
    Guid TeamAthleteId,
    HealthMetricType MetricType,
    decimal MetricValue): IRequest<Unit>;