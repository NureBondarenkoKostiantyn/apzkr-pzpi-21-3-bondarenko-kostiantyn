using AutoMapper;
using TrainSmart.Common.DTOs.Sport;

namespace TrainSmart.Application.MappingProfiles;

public class SportMappingProfile: Profile
{
    public SportMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.Sport, SportDto>();
    }
}