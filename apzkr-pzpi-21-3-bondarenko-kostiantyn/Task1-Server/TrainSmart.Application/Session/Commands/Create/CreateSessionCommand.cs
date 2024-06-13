using MediatR;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Commands.Create;

public record CreateSessionCommand(
    Guid TeamId,
    int Duration): IRequest<SessionDto>;