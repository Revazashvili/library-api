using Application.Common.Models;
using Application.Features;
using Domain.Entities;
using MediatR;

namespace API.Endpoints;

internal static class AuthorsEndpointsMapper
{
    internal static IEndpointRouteBuilder MapAuthors(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authorsRouteGroupBuilder = endpointRouteBuilder.MapGroup("authors");

        authorsRouteGroupBuilder.MapPost("/", async (AddAuthor.Command addAuthorCommand, 
            IMediator mediator) => await mediator.Send(addAuthorCommand))
            .Produces<IResponse<Author>>();

        authorsRouteGroupBuilder.MapDelete("/{id}", async (int id,
            IMediator mediator) => await mediator.Send(new DeleteAuthor.Command(id)))
            .Produces<IResponse<Unit>>();

        return authorsRouteGroupBuilder;
    }
}