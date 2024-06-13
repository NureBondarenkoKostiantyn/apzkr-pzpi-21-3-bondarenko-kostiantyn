using MediatR;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Queries.GetSessionById;

public record GetSessionByIdQuery(
    Guid Id): IRequest<SessionDto>;