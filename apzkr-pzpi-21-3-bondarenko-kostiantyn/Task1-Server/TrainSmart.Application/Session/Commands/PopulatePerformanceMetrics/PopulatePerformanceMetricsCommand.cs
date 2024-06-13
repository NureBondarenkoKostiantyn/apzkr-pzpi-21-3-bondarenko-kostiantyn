using MediatR;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Commands.PopulatePerformanceMetrics;

public record PopulatePerformanceMetricsCommand(
    Guid SessionId,
    Guid TeamAthleteId,
    List<PerformanceMetricTypeValueDto> Metrics): IRequest<Unit>;