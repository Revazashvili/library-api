using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    public BookRepository(LibraryDbContext context) => _context = context;

    public Task<List<Book>> GetAllAsync(Pagination? pagination = null,CancellationToken cancellationToken = default) =>
        _context.Books
            .Include(book => book.Author)
            .Paged(pagination)
            .ToListAsync(cancellationToken);

    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Books
            .Include(book => book.Author)
            .FirstOrDefaultAsync(book => book.Id == id, cancellationToken);

    public async Task AddAsync(Book entity, CancellationToken cancellationToken = default) =>
        await _context.Books.AddAsync(entity, cancellationToken);

    public void Update(Book entity) => _context.Books.Update(entity);

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = await GetByIdAsync(id, cancellationToken);
        if (book is null)
            return;
        _context.Books.Remove(book);
    }

    public Task<List<Book>> GetAllByAuthorAsync(int authorId, Pagination? pagination = null,
        CancellationToken cancellationToken = default) =>
        _context.Books
            .Where(book => book.Author.Id == authorId)
            .AsNoTracking()
            .Paged(pagination)
            .ToListAsync(cancellationToken);
}