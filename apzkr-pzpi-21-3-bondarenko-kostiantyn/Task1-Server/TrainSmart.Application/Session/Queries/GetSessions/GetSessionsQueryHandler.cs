using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Queries.GetSessions;

public class GetSessionsQueryHandler: IRequestHandler<GetSessionsQuery, List<SessionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSessionsQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SessionDto>> Handle(
        GetSessionsQuery request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.TeamId, true, cancellationToken);
        if (team is null)
        {
            throw new ApplicationException("Team was not found");
        }
        
        var sessions = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByTeamId(request.TeamId, true, cancellationToken);
        
        return _mapper.Map<List<SessionDto>>(sessions);
    }
}