using System.Collections.Immutable;
using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.Team.Queries.GetAll;

public class GetAllTeamsQueryHandler: IRequestHandler<GetAllTeamsQuery, List<TeamDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTeamsQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TeamDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var sports = (await _unitOfWork
                .GetRepository<ISportRepository>()
                .GetAllAsync(true, cancellationToken))
            .ToImmutableDictionary(x => x.Id, x => x);
        
        var teams = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetAllAsync(true, cancellationToken);

        var mappedTeams = _mapper.Map<List<TeamDto>>(teams);
        foreach (var team in mappedTeams)
        {
            var sportName = sports.GetValueOrDefault(team.SportId)?.Name;
            team.SportName = sportName;
        }

        return mappedTeams;
    }
}