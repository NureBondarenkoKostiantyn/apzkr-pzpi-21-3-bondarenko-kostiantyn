using MediatR;
using TrainSmart.Application.Abstractions.Persistence;

namespace TrainSmart.Application.Session.Commands.Delete;

public class DeleteSessionCommandHandler: IRequestHandler<DeleteSessionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSessionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteSessionCommand request, 
        CancellationToken cancellationToken)
    {
        var session = await _unitOfWork
            .GetRepository<ISessionRepository>()
            .GetByIdAsync(request.Id, false, cancellationToken);
        if (session is null)
        {
            throw new ApplicationException("Session was not found");
        }

        _unitOfWork
            .GetRepository<ISessionRepository>()
            .Delete(session);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}