using MediatR;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Athlete.Queries.GetById;

public record GetAthleteByIdQuery(Guid Id): IRequest<AthleteDto>;