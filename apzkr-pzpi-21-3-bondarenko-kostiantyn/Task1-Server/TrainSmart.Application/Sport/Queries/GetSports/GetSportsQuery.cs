using MediatR;
using TrainSmart.Common.DTOs.Sport;

namespace TrainSmart.Application.Sport.Queries.GetSports;

public record GetSportsQuery: IRequest<List<SportDto>>;