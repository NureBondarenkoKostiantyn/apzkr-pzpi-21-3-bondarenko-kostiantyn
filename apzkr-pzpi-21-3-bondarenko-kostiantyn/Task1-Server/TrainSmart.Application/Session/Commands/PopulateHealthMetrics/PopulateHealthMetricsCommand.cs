using MediatR;
using TrainSmart.Common.DTOs.Session;
using TrainSmart.Domain.Enums;

namespace TrainSmart.Application.Session.Commands.PopulateHealthMetrics;

public record PopulateHealthMetricsCommand(
    Guid SessionId,
    Guid TeamAthleteId,
    List<HealthMetricTypeValueDto> Metrics): IRequest<Unit>;