using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Commands.Create;

public class CreateTeamCommandHandler: IRequestHandler<CreateTeamCommand, TeamDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTeamCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(
        CreateTeamCommand request, 
        CancellationToken cancellationToken)
    {
        var team = new Domain.AggregateRoots.Team(
            request.Name, request.Description, request.CountryName, request.SportId);
        
        await _unitOfWork
            .GetRepository<ITeamRepository>()
            .CreateAsync(team, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeamDto>(team);
    }
}