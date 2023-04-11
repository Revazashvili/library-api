using Application.Common.Models;
using Application.Features.Books;
using MediatR;

namespace API.Endpoints;

internal static class BooksEndpointsMapper
{
    internal static IEndpointRouteBuilder MapBooks(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var booksRouteGroupBuilder = endpointRouteBuilder.MapGroup("books");

        booksRouteGroupBuilder.MapPost("/", async (AddBookCommand command, 
            CancellationToken cancellationToken, ISender sender) => await sender.Send(command,cancellationToken))
            .Produces<IResponse<BookResponse>>();

        return booksRouteGroupBuilder;
    }
}