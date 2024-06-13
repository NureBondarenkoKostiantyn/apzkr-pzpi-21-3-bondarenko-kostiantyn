using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Athlete.Commands.BindToUser;

public class BindAthleteToUserCommandHandler: IRequestHandler<BindAthleteToUserCommand, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BindAthleteToUserCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AthleteDto> Handle(
        BindAthleteToUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .GetRepository<IUserRepository>()
            .GetByIdAsync(request.UserId, false, cancellationToken);
        if (user is null)
        {
            throw new ApplicationException("User was not found");
        }

        var athlete = new Domain.AggregateRoots.Athlete(user.Id);

        await _unitOfWork
            .GetRepository<IAthleteRepository>()
            .CreateAsync(athlete, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AthleteDto>(athlete);
    }
}