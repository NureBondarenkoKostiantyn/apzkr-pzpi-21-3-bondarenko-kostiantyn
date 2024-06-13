using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.User;

namespace TrainSmart.Application.User.Queries.Get;

public class GetAllUsersQueryHandler: IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork
            .GetRepository<IUserRepository>()
            .GetAllAsync(true, cancellationToken);

        return _mapper.Map<List<UserDto>>(users);
    }
}