using MediatR;
using TrainSmart.Common.DTOs.Athlete;

namespace TrainSmart.Application.Athlete.Commands.BindToUser;

public record BindAthleteToUserCommand: IRequest<AthleteDto>
{
    public Guid UserId { get; set; }
};