using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features.Books;

public record UpdateBookCommand(int Id,UpdateBookRequest UpdateBookRequest) : ICommand<BookResponse>;

internal class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand,BookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public Task<IResponse<BookResponse>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var updateBookRequest = request.UpdateBookRequest;
        var book = Book.Create(updateBookRequest.Title, updateBookRequest.Description, request.Id);
        _unitOfWork.Books.Update(book);
        _unitOfWork.CommitAsync();

        var bookResponse = BookResponse.Create(book);
        return Task.FromResult(Response.Success(bookResponse));
    }
}