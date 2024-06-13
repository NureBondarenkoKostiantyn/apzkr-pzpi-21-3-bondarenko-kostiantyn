using MediatR;

namespace TrainSmart.Application.User.Commands.Delete;

public record DeleteUserCommand(
    Guid Id): IRequest<Unit>;