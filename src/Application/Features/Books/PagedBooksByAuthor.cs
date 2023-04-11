using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;

namespace Application.Features.Books;

public record PagedBooksByAuthorQuery(int AuthorId,Pagination? Pagination = null) 
    : IQuery<IEnumerable<BookResponse>>;

internal class PagedBooksByAuthorQueryHandler : IQueryHandler<PagedBooksByAuthorQuery,IEnumerable<BookResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public PagedBooksByAuthorQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<IEnumerable<BookResponse>>> Handle(PagedBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetAllByAuthorAsync(request.AuthorId, request.Pagination,
            cancellationToken);

        var bookResponses = books
            .Select(BookResponse.Create)
            .ToList();
        return Response.Success<IEnumerable<BookResponse>>(bookResponses);
    }
}