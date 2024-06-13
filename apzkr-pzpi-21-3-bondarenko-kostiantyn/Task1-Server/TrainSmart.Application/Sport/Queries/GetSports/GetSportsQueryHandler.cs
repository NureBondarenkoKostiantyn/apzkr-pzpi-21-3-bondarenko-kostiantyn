using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Sport;

namespace TrainSmart.Application.Sport.Queries.GetSports;

public class GetSportsQueryHandler: IRequestHandler<GetSportsQuery, List<SportDto>>
{
    private readonly ISportRepository _sportRepository;
    private readonly IMapper _mapper;

    public GetSportsQueryHandler(
        ISportRepository sportRepository, 
        IMapper mapper)
    {
        _sportRepository = sportRepository;
        _mapper = mapper;
    }

    public async Task<List<SportDto>> Handle(
        GetSportsQuery request, 
        CancellationToken cancellationToken)
    {
        var sports = await _sportRepository.GetAllAsync(true, cancellationToken);
        return _mapper.Map<List<SportDto>>(sports);
    }
}