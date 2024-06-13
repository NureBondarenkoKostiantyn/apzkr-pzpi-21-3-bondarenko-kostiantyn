using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Queries.GetById;

public class GetTeamByIdQueryHandler: IRequestHandler<GetTeamByIdQuery, TeamDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeamByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(
        GetTeamByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.Id, true, cancellationToken);
        if (team is null)
        {
            throw new ApplicationException("Team was not found");
        }

        var sport = await _unitOfWork
            .GetRepository<ISportRepository>()
            .GetByIdAsync(team.SportId, true, cancellationToken);
        
        var mappedTeam = _mapper.Map<TeamDto>(team);
        mappedTeam.SportName = sport?.Name;

        return mappedTeam;
    }
}