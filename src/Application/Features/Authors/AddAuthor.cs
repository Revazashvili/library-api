using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Authors;

public record AddAuthorCommand(string FirstName, string LastName) : ICommand<Author>;

internal class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand, Author>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddAuthorCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author(request.FirstName, request.LastName);
        await _unitOfWork.Authors.AddAsync(author, cancellationToken);
        await _unitOfWork.CommitAsync();

        return Response.Success(author);
    }
}

public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithMessage("FirstName must not be null.")
            .NotNull()
            .WithMessage("FirstName must not be empty.")
            .Length(0, 250)
            .WithMessage("FirstName must not be more than 250 character.");
        
        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithMessage("LastName must not be null.")
            .NotNull()
            .WithMessage("LastName must not be empty.")
            .Length(0, 250)
            .WithMessage("LastName must not be more than 250 character.");
    }
}