using Application.Common.Either;
using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.Common.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Books;

public record DeleteBookCommand(int Id) : IValidatedCommand<Unit>;

internal class DeleteBookCommandHandler : IValidatedCommandHandler<DeleteBookCommand,Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Either<Unit,ValidationResult>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.Books.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
        catch (Exception)
        {
            return new ValidationResult("Error occured while deleting book");
        }
    }
}


public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Id)
            .NotNull()
            .WithMessage("Id must not be null.")
            .NotEqual(0)
            .WithMessage("Id must not equal to zero.")
            .MustAsync(async (id, cancellationToken) =>
                await unitOfWork.Books.ExistsWithIdAsync(id, cancellationToken))
            .WithMessage("Book doesn't exists with specified id.");
    }
}