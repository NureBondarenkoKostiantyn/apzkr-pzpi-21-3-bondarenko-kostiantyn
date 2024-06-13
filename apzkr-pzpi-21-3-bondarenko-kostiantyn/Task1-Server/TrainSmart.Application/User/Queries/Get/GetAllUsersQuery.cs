using MediatR;
using TrainSmart.Common.DTOs.User;

namespace TrainSmart.Application.User.Queries.Get;

public record GetAllUsersQuery: IRequest<List<UserDto>>;