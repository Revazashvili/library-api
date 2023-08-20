using Application.Common.Either;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Validation;
using Application.Common.Wrappers;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Books;

public record UpdateBookCommand(int Id,UpdateBookRequest UpdateBookRequest) : IValidatedCommand<BookResponse>;

internal class UpdateBookCommandHandler : IValidatedCommandHandler<UpdateBookCommand,BookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public Task<Either<BookResponse,ValidationResult>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var updateBookRequest = request.UpdateBookRequest;
            var book = Book.Create(updateBookRequest.Title, updateBookRequest.Description, request.Id);
            _unitOfWork.Books.Update(book);
            _unitOfWork.CommitAsync();

            var bookResponse = BookResponse.Create(book);
            return Task.FromResult(new Either<BookResponse, ValidationResult>(bookResponse));
        }
        catch (Exception)
        {
            var validationResult = new ValidationResult("Error occured while updating book");
            return Task.FromResult(new Either<BookResponse, ValidationResult>(validationResult));
        }
    }
}

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Id)
            .NotNull()
            .WithMessage("Id must not be null.")
            .NotEqual(0)
            .WithMessage("Id must not equal to zero.")
            .MustAsync(async (id, cancellationToken) =>
                await unitOfWork.Books.ExistsWithIdAsync(id, cancellationToken))
            .WithMessage("Book doesn't exists with specified id.");;
        
        RuleFor(command => command.UpdateBookRequest.Title)
            .NotNull()
            .WithMessage("Title must not be null.")
            .NotEmpty()
            .WithMessage("Title must not be empty.")
            .Length(0, 1000)
            .WithMessage("Title must not be more than 1000 character.");;
        
        RuleFor(command => command.UpdateBookRequest.Description)
            .NotNull()
            .WithMessage("Description must not be null.")
            .NotEmpty()
            .WithMessage("Description must not be empty.");
    }
}