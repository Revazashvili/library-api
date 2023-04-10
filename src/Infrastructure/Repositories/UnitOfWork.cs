using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
        Authors = new AuthorRepository(context);
        Books = new BookRepository(context);
    }

    public IAuthorRepository Authors { get; }
    public IBookRepository Books { get; }

    public Task<int> CommitAsync() => _context.SaveChangesAsync();
    public void RejectChanges()
    {
        var entityEntries = _context.ChangeTracker
            .Entries()
            .Where(e => e.State != EntityState.Unchanged);
        foreach (var entry in entityEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
}