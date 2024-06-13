using MediatR;
using TrainSmart.Common.DTOs.User;

namespace TrainSmart.Application.User.Queries.GetById;

public record GetUserByIdQuery(Guid Id): IRequest<UserDto>;