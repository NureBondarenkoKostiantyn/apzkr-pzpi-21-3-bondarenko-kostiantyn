using AutoMapper;
using TrainSmart.Common.DTOs.Team;

namespace TrainSmart.Application.MappingProfiles;

public class TeamMappingProfile: Profile
{
    public TeamMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.Team, TeamDto>();
    }
}