using AutoMapper;
using TrainSmart.Common.DTOs.Session;

namespace TrainSmart.Application.MappingProfiles;

public class SessionMappingProfile: Profile
{
    public SessionMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.Session, SessionDto>();
    }
}