using AutoMapper;
using TrainSmart.Common.DTOs.Auth;

namespace TrainSmart.Application.MappingProfiles;

public class AuthMappingProfile: Profile
{
    public AuthMappingProfile()
    {
        CreateMap<SignupDto, Domain.AggregateRoots.User>();
    }
}