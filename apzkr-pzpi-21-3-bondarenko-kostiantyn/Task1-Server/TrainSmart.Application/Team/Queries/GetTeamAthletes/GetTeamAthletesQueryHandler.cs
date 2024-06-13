using MediatR;
using TrainSmart.Application.Abstractions.Persistence;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Team.Queries.GetTeamAthletes;

public class GetTeamAthletesQueryHandler: IRequestHandler<GetTeamAthletesQuery, IEnumerable<AthleteDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTeamAthletesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AthleteDto>> Handle(
        GetTeamAthletesQuery request, 
        CancellationToken cancellationToken)
    {
        var team = await _unitOfWork
            .GetRepository<ITeamRepository>()
            .GetByIdAsync(request.TeamId, cancellationToken: cancellationToken);
        if (team is null)
        {
            throw new ApplicationException("Team was not found");
        }

        var athleteDtos = new List<AthleteDto>();
        
        var athletes = await _unitOfWork
            .GetRepository<IAthleteRepository>()
            .GetByIdsAsync(team.Athletes.Select(x => x.AthleteId), cancellationToken);
        foreach (var athlete in athletes)
        {
            var user = await _unitOfWork
                .GetRepository<IUserRepository>()
                .GetByIdAsync(athlete.UserId, cancellationToken: cancellationToken);
            if (user is null)
            {
                throw new ApplicationException("User was not found");
            }
            
            athleteDtos.Add(new AthleteDto
            {
                Id = athlete.Id,
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        return athleteDtos;
    }
}