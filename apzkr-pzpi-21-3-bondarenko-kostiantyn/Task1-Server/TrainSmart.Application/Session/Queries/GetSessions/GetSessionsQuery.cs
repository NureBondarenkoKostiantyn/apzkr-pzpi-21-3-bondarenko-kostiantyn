using MediatR;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Queries.GetSessions;

public record GetSessionsQuery(
    Guid TeamId): IRequest<List<SessionDto>>;