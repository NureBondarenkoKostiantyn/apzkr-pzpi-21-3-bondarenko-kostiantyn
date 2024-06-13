using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Commands.Update;

public class UpdateTeamCommandHandler: IRequestHandler<UpdateTeamCommand, TeamDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTeamCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(
        UpdateTeamCommand request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.Id, false, cancellationToken);
        if (team is null)
        {
            throw new ApplicationException("Team was not found");
        }

        team.Name = request.Name;
        team.Description = request.Description;
        team.CountryName = request.CountryName;

        _unitOfWork
            .GetRepository<ITeamRepository>()
            .Update(team);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeamDto>(team);
    }
}