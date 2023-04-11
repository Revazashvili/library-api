using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features.Books;

public record AddBookCommand(string Title, string Description, int AuthorId) 
    : ICommand<BookResponse>;

internal class AddBookCommandHandler : ICommandHandler<AddBookCommand, BookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<BookResponse>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId, cancellationToken);

        var book = Book.Create(request.Title, request.Description);
        if (author!.Books is null)
            author.Books = new List<Book>();

        author.Books?.Add(book);
        await _unitOfWork.CommitAsync();

        var response = BookResponse.Create(book);
        return Response.Success(response);
    }
}