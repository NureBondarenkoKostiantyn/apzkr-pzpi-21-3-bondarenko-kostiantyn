using AutoMapper;
using TrainSmart.Common.DTOs.User;

namespace TrainSmart.Application.MappingProfiles;

public class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        CreateMap<Domain.AggregateRoots.User, UserDto>();
    }
}