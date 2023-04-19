using Application.Common.Models;
using Application.Features.Authors;
using Domain.Entities;
using MediatR;

namespace API.Endpoints;

internal static class AuthorsEndpointsMapper
{
    internal static IEndpointRouteBuilder MapAuthors(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authorsRouteGroupBuilder = endpointRouteBuilder.MapGroup("authors");

        authorsRouteGroupBuilder.MapPost("/", async (AddAuthorCommand addAuthorCommand, CancellationToken cancellationToken,
                ISender sender) => await sender.Send(addAuthorCommand,cancellationToken))
            .Produces<IResponse<Author>>();

        authorsRouteGroupBuilder.MapDelete("/{id:int}", async (int id, CancellationToken cancellationToken,
                ISender sender) => await sender.Send(new DeleteAuthorCommand(id),cancellationToken))
            .Produces<IResponse<Unit>>();

        return authorsRouteGroupBuilder;
    }
}