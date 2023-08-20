using API.Extensions;
using Application.Common.Validation;
using Application.Features.Authors;
using Domain.Entities;
using MediatR;

namespace API.Endpoints;

internal static class AuthorsEndpointsMapper
{
    internal static IEndpointRouteBuilder MapAuthors(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authorsRouteGroupBuilder = endpointRouteBuilder.MapGroup("authors");

        authorsRouteGroupBuilder.MapPost("/", async (AddAuthorCommand addAuthorCommand,
                CancellationToken cancellationToken,ISender sender) => 
                (await sender.Send(addAuthorCommand, cancellationToken)).ToResult())
            .Produces<Author>()
            .Produces<ValidationResult>(StatusCodes.Status400BadRequest);

        authorsRouteGroupBuilder.MapDelete("/{id:int}", async (int id, CancellationToken cancellationToken,
                ISender sender) => (await sender.Send(new DeleteAuthorCommand(id),cancellationToken)).ToResult())
            .Produces<Unit>()
            .Produces<ValidationResult>(StatusCodes.Status400BadRequest);

        return authorsRouteGroupBuilder;
    }
}