using Application.Common.Either;
using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.Common.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Authors;

public record DeleteAuthorCommand(int Id) : IValidatedCommand<Unit>;

internal class DeleteAuthorCommandHandler : IValidatedCommandHandler<DeleteAuthorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Either<Unit,ValidationResult>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.Authors.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
        catch (Exception exception)
        {
            return new ValidationResult(exception.Message);
        }
    }
}

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(command => command.Id)
            .NotNull()
            .WithMessage("Id must not be null.")
            .NotEqual(0)
            .WithMessage("Id must not equal to zero.")
            .MustAsync(async (id, cancellationToken) =>
                await unitOfWork.Authors.ExistsWithIdAsync(id, cancellationToken))
            .WithMessage("Author doesn't exists with specified id.");
    }
}