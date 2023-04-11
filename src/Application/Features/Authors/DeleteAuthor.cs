using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using MediatR;

namespace Application.Features.Authors;

public record DeleteAuthorCommand(int Id) : ICommand<Unit>;

internal class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<Unit>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Authors.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync();

        return Response.Success(Unit.Value);
    }
}