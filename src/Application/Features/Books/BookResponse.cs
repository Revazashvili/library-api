using Domain.Entities;

namespace Application.Features.Books;

public record BookResponse(int Id, string Title, string Description)
{
    public static BookResponse Create(Book book) => new(book.Id, book.Title, book.Description);
}