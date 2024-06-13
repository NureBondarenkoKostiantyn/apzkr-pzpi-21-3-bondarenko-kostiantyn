using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.User;

namespace TrainSmart.Application.User.Queries.GetById;

public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .GetRepository<IUserRepository>()
            .GetByIdAsync(request.Id, true, cancellationToken);
        if (user is null)
        {
            throw new ApplicationException("User was not found");
        }

        return _mapper.Map<UserDto>(user);
    }
}