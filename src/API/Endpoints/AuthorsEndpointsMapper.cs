using Application.Features;
using MediatR;

namespace API.Endpoints;

internal static class AuthorsEndpointsMapper
{
    internal static IEndpointRouteBuilder MapAuthors(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authorsRouteGroupBuilder = endpointRouteBuilder.MapGroup("authors");

        authorsRouteGroupBuilder.MapPost("/", async (AddAuthor.Command author, 
            IMediator mediator) => await mediator.Send(author));

        return authorsRouteGroupBuilder;
    }
}