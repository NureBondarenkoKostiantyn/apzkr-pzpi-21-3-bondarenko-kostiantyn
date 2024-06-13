using TrainSmart.Domain.AggregateRoots;
using TrainSmart.Domain.Entities;

namespace TrainSmart.Application.Abstractions.Persistence;

public interface ITrainingSessionRepository: IGenericRepository<Session>
{
}