using AutoMapper;
using TrainSmart.Common.DTOs.PerformanceMetric;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.MappingProfiles;

public class PerformanceMetricMappingProfile: Profile
{
    public PerformanceMetricMappingProfile()
    {
        CreateMap<PerformanceMetric, PerformanceMetricDto>();
    }
}