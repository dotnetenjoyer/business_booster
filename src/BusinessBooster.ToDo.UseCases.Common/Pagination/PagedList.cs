namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// The paged list. Contains truncated items.
/// </summary>
/// <typeparam name="T">Type of contained items.</typeparam>
public class PagedList<T> : OffsetLimitedList<T>
{
    /// <summary>
    /// The number of page.
    /// </summary>
    public int Page { get; }
    
    /// <summary>
    /// The number of page size.
    /// </summary>
    public int PageSize { get; }
    
    /// <summary>
    /// The number of total pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="items">Truncated items.</param>
    /// <param name="page">The number of page.</param>
    /// <param name="pageSize">The number of page size.</param>
    /// <param name="totalCount">The number of all items.</param>
    public PagedList(ICollection<T> items, int page, int pageSize, int totalCount) : base(items, (page - 1) * pageSize, pageSize, totalCount)
    {
        if (page < PaginationHelper.FirstPage)
        {
            throw new ArgumentOutOfRangeException(nameof(page));
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Round(totalCount / (double)PageSize, MidpointRounding.ToPositiveInfinity);
    }
}