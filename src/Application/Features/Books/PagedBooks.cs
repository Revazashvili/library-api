using Application.Common.Either;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Validation;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features.Books;

public record PagedBooksQuery(Pagination Pagination) : IValidatedQuery<IEnumerable<Book>>;

internal class PagedBooksCommandHandler : IValidatedQueryHandler<PagedBooksQuery,IEnumerable<Book>>
{
    private readonly IUnitOfWork _unitOfWork;
    public PagedBooksCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Either<IEnumerable<Book>,ValidationResult>> Handle(PagedBooksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books =  await _unitOfWork.Books.GetAllAsync(request.Pagination, cancellationToken);
            if(!books.Any())
                return new ValidationResult("Can't find any book");

            return books;
        }
        catch (Exception)
        {
            return new ValidationResult("Error occured while retrieving books");
        }
    }
}