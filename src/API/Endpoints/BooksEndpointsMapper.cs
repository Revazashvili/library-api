using API.Extensions;
using Application.Common.Models;
using Application.Common.Validation;
using Application.Features.Books;
using Domain.Entities;
using MediatR;

namespace API.Endpoints;

internal static class BooksEndpointsMapper
{
    internal static IEndpointRouteBuilder MapBooks(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var booksRouteGroupBuilder = endpointRouteBuilder.MapGroup("books");

        booksRouteGroupBuilder.MapGet("", async (int pageNumber, int pageSize, CancellationToken cancellationToken,
                ISender sender) => 
                (await sender.Send(new PagedBooksQuery(new Pagination(pageNumber, pageSize)), cancellationToken)).ToResult())
            .Produces<IEnumerable<Book>>()
            .Produces<ValidationResult>(StatusCodes.Status400BadRequest);

        booksRouteGroupBuilder.MapGet("/{authorId:int}", async (int authorId, int pageNumber, int pageSize,
                    CancellationToken cancellationToken,
                    ISender sender) =>
                (await sender.Send(new PagedBooksByAuthorQuery(authorId, new Pagination(pageNumber, pageSize)),
                    cancellationToken)).ToResult())
            .Produces<IEnumerable<Book>>()
            .Produces<ValidationResult>(StatusCodes.Status400BadRequest);
        
        booksRouteGroupBuilder.MapPost("/", async (AddBookCommand command, 
            CancellationToken cancellationToken, ISender sender) => await sender.Send(command,cancellationToken))
            .Produces<IResponse<BookResponse>>();
        
        booksRouteGroupBuilder.MapPatch("/{id:int}", async (int id,UpdateBookRequest request, CancellationToken cancellationToken,
                ISender sender) => await sender.Send(new UpdateBookCommand(id,request),cancellationToken))
            .Produces<IResponse<BookResponse>>();
        
        booksRouteGroupBuilder.MapDelete("/{id:int}", async (int id, CancellationToken cancellationToken, 
                    ISender sender) => await sender.Send(new DeleteBookCommand(id),cancellationToken))
            .Produces<IResponse<Unit>>();

        return booksRouteGroupBuilder;
    }
}