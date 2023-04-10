using Application.Common.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IAuthorRepository authors,
        IBookRepository books)
    {
        Authors = authors;
        Books = books;
    }

    public IAuthorRepository Authors { get; }
    public IBookRepository Books { get; }
}