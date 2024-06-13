using Bogus;
using TrainSmart.Domain.Entities;
using TrainSmart.Domain.Enums;

namespace TrainSmart.Functions.Services;

public sealed class PerformanceMetricsFakerService: Faker<PerformanceMetric>
{
    public PerformanceMetricsFakerService()
    {
        RuleFor(x => x.MetricType, faker => faker.PickRandom<PerformanceMetricType>());
        RuleFor(x => x.MetricValue, faker => faker.Random.Decimal(0, 100));
    }

    public List<PerformanceMetric> GenerateRecords(int count)
    {
        return Generate(count);
    }
}