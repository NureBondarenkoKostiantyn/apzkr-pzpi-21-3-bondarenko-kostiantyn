using AutoMapper;
using TrainSmart.Common.DTOs.HealthMetric;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.MappingProfiles;

public class HealthMetricMappingProfile: Profile
{
    public HealthMetricMappingProfile()
    {
        CreateMap<HealthMetric, HealthMetricDto>();
    }
}