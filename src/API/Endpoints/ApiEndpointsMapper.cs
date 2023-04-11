﻿namespace API.Endpoints;

internal static class ApiEndpointsMapper
{
    internal static IEndpointRouteBuilder MapApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var apiRouteGroupBuilder = endpointRouteBuilder.MapGroup("api");
        return apiRouteGroupBuilder;
    }
}