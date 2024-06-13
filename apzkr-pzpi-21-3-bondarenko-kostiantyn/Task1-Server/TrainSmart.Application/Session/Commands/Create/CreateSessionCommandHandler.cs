using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Commands.Create;

public class CreateSessionCommandHandler: IRequestHandler<CreateSessionCommand, SessionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSessionCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SessionDto> Handle(
        CreateSessionCommand request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.TeamId, false, cancellationToken);
        if (team is null)
        {
            throw new ApplicationException("Team was not found");
        }

        var teamSessions = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByTeamId(team.Id, true, cancellationToken);
        var startedTeamSession = teamSessions.FirstOrDefault(x => x.EndDate > DateTime.UtcNow);
        if (startedTeamSession is not null)
        {
            throw new ApplicationException($"Started session has not finished yet. It will finish at {startedTeamSession.EndDate}");
        }
        
        var session = new Domain.AggregateRoots.Session(team.Id, request.Duration);

        await _unitOfWork
            .GetRepository<ISessionRepository>()
            .CreateAsync(session, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SessionDto>(session);
    }
}