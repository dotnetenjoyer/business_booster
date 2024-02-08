namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// Contains useful members to pagination purposes.
/// </summary>
public static class PaginationHelper
{
    /// <summary>
    /// First page index.
    /// </summary>
    public const int FirstPage = 1;
    
    /// <summary>
    /// Calculates pagination offset.
    /// </summary>
    /// <param name="page">The number of page.</param>
    /// <param name="pageSize">The number of page size.</param>
    /// <returns>The offset.</returns>
    public static int CalculateOffset(int page, int pageSize)
    {
        if (page < FirstPage)
        {
            throw new ArgumentOutOfRangeException(nameof(page));
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        return (page - 1) * pageSize;
    }
}