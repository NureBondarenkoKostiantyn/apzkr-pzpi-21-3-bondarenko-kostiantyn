using System.Collections.Immutable;
using AutoMapper;
using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Athlete.Queries.Get;

public class GetAllAthletesQueryHandler: IRequestHandler<GetAllAthletesQuery, List<AthleteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAthletesQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AthleteDto>> Handle(GetAllAthletesQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _unitOfWork
            .GetRepository<IAthleteRepository>()
            .GetAllAsync(true, cancellationToken);

        var users = (await _unitOfWork
                .GetRepository<IUserRepository>()
                .GetAllAsync(true, cancellationToken))
            .ToImmutableDictionary(x => x.Id, x => x);

        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        foreach (var mappedAthlete in mappedAthletes)
        {
            var user = users.GetValueOrDefault(mappedAthlete.UserId);
            if (user is null)
            {
                continue;
            }
            
            mappedAthlete.Email = user.Email!;
            mappedAthlete.FirstName = user.FirstName;
            mappedAthlete.LastName = user.LastName;
        }

        return mappedAthletes;
    }
}