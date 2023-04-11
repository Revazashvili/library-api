using Application.Common.Models;

namespace Infrastructure.Extensions;

internal static class PaginationExtensions
{
    internal static IQueryable<T> Paged<T>(this IQueryable<T> queryable,Pagination? pagination)
    {
        if(pagination is null)
            return queryable;

        var skip = (pagination.PageNumber - 1) * pagination.PageSize;
        return queryable
            .Skip(skip)
            .Take(pagination.PageSize);
    }
}