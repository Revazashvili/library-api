using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetAllByAuthorAsync(int authorId,Pagination pagination,CancellationToken cancellationToken = default);
}