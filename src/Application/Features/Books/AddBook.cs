using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using FluentValidation;

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

public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage("Title must not be empty.")
            .NotNull()
            .WithMessage("Title must not be null.")
            .Length(0, 1000)
            .WithMessage("Title must not be more than 1000 character.");
        
        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Description must not be empty.")
            .NotNull()
            .WithMessage("Description must not be null.");

        RuleFor(command => command.AuthorId)
            .NotNull()
            .WithMessage("FirstName must not be null.")
            .MustAsync(async (authorId, cancellationToken) =>
                await unitOfWork.Authors.ExistsWithIdAsync(authorId, cancellationToken));
    }
}