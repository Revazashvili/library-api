﻿using Application.Common.Either;
using Application.Common.Interfaces;
using Application.Common.Validation;
using Application.Common.Wrappers;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Books;

public record AddBookCommand(string Title, string Description, int AuthorId) 
    : IValidatedCommand<BookResponse>;

internal class AddBookCommandHandler : IValidatedCommandHandler<AddBookCommand, BookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Either<BookResponse,ValidationResult>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId, cancellationToken);

            var book = Book.Create(request.Title, request.Description);
            if (author!.Books is null)
                author.Books = new List<Book>();

            author.Books?.Add(book);
            await _unitOfWork.CommitAsync();

            return BookResponse.Create(book);
        }
        catch (Exception)
        {
            return new ValidationResult("Error occured while adding book");
        }
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
                await unitOfWork.Authors.ExistsWithIdAsync(authorId, cancellationToken))
            .WithMessage("Author with this id doesn't exists");
    }
}