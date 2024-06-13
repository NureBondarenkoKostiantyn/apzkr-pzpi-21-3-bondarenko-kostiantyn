using AutoMapper;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.MappingProfiles;

public class AthleteMappingProfile: Profile
{
    public AthleteMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.Athlete, AthleteDto>();
    }
}