﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;
    public AuthorRepository(LibraryDbContext context) => _context = context;

    public Task<List<Author>> GetAllAsync(Pagination pagination,CancellationToken cancellationToken = default) => 
        _context.Authors
            .Include(author => author.Books)
            .Paged(pagination)
            .ToListAsync(cancellationToken);

    public Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Authors
            .Include(author => author.Books)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Author entity, CancellationToken cancellationToken = default) =>
        await _context.Authors.AddAsync(entity, cancellationToken);

    public void Update(Author entity) => _context.Authors.Update(entity);

    public async Task DeleteAsync(int id,CancellationToken cancellationToken = default)
    {
        var author = await GetByIdAsync(id, cancellationToken);
        if (author is null)
            return;
        _context.Authors.Remove(author);
    }

    public Task<bool> ExistsWithIdAsync(int id,CancellationToken cancellationToken = default) => 
        _context.Authors.AnyAsync(author => author.Id == id, cancellationToken: cancellationToken);
}