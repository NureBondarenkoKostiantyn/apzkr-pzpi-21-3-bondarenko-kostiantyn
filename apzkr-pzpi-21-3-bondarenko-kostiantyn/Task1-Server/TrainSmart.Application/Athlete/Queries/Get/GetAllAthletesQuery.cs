using MediatR;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Athlete.Queries.Get;

public record GetAllAthletesQuery: IRequest<List<AthleteDto>>;