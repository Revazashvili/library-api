using Application.Common.Either;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Validation;
using Application.Common.Wrappers;

namespace Application.Features.Books;

public record PagedBooksByAuthorQuery(int AuthorId,Pagination Pagination) 
    : IValidateQuery<IEnumerable<BookResponse>>;

internal class PagedBooksByAuthorQueryHandler : IValidatedQueryHandler<PagedBooksByAuthorQuery,IEnumerable<BookResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public PagedBooksByAuthorQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Either<IEnumerable<BookResponse>,ValidationResult>> Handle(PagedBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books = await _unitOfWork.Books.GetAllByAuthorAsync(request.AuthorId, request.Pagination,
                cancellationToken);

            if(!books.Any())
                return new ValidationResult("Can't find any book for this author");
            
            return books
                .Select(BookResponse.Create)
                .ToList();
        }
        catch (Exception exception)
        {
            return new ValidationResult("Error occured while retrieving books");
        }
    }
}