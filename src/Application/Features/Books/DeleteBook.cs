using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using MediatR;

namespace Application.Features.Books;

public record DeleteBookCommand(int Id) : ICommand<Unit>;

internal class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand,Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<Unit>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Books.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync();

        return Response.Success(Unit.Value);
    }
}