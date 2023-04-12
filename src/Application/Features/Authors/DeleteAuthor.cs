using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using FluentValidation;
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