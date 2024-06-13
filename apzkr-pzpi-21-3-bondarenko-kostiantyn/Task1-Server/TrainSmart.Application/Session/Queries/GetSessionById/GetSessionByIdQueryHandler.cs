using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.Session.Queries.GetSessionById;

public class GetSessionByIdQueryHandler: IRequestHandler<GetSessionByIdQuery, SessionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSessionByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SessionDto> Handle(
        GetSessionByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var session = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByIdAsync(request.Id, true, cancellationToken);
        if (session is null)
        {
            throw new ApplicationException("Session was not found");
        }

        return _mapper.Map<SessionDto>(session);
    }
}