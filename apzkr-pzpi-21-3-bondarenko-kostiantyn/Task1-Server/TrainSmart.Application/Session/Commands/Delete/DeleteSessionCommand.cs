using MediatR;

namespace TrainSmart.Application.Session.Commands.Delete;

public record DeleteSessionCommand(Guid Id): IRequest<Unit>;