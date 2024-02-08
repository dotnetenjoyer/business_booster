using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// Contains factory methods to create paged list.
/// </summary>
public static class EFCorePagedListFactory
{
    /// <summary>
    /// Creates paged list from IQueryable source.
    /// </summary>
    /// <param name="source">Source query.</param>
    /// <param name="page">The number of page.</param>
    /// <param name="pageSize">The number of page size.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <typeparam name="T">The type of items.</typeparam>
    /// <returns>The paged list.</returns>
    public static async Task<PagedList<T>> CreateAsync<T>(IQueryable<T> source, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var offset = PaginationHelper.CalculateOffset(page, pageSize);
        var items = await source.Skip(offset).Take(pageSize).ToListAsync(cancellationToken);
        var totalCount = await source.CountAsync(cancellationToken);
        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}