using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features.Books;

public record PagedBooksQuery(Pagination Pagination) : IQuery<IEnumerable<Book>>;

internal class PagedBooksCommandHandler : IQueryHandler<PagedBooksQuery,IEnumerable<Book>>
{
    private readonly IUnitOfWork _unitOfWork;
    public PagedBooksCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<IEnumerable<Book>>> Handle(PagedBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetAllAsync(request.Pagination, cancellationToken);
        return Response.Success(books);
    }
}