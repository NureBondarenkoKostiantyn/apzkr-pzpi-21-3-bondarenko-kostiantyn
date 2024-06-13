using MediatR;
using TrainSmart.Application.Abstractions.Persistence;

namespace TrainSmart.Application.User.Commands.Delete;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .GetRepository<IUserRepository>()
            .GetByIdAsync(request.Id, false, cancellationToken);
        if (user is null)
        {
            throw new ApplicationException("User was not found");
        }

        _unitOfWork
            .GetRepository<IUserRepository>()
            .Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}