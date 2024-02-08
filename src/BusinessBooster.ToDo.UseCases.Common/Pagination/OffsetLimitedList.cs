using System.Collections;

namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// The class that represents a part of truncated collection.
/// </summary>
/// <typeparam name="T">Collection item type.</typeparam>
public class OffsetLimitedList<T> : IEnumerable<T>
{
    /// <summary>
    /// Truncated items.
    /// </summary>
    public ICollection<T> Items { get; }
    
    /// <summary>
    /// The number of skipped items.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// The maximum number of taken items.
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// The number of all items.
    /// </summary>
    public int TotalCount { get; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="items">Truncated items.</param>
    /// <param name="offset">The number of skipped items.</param>
    /// <param name="limit">The maximum number of taken items.</param>
    /// <param name="totalCount">The number of all items.</param>
    public OffsetLimitedList(ICollection<T> items, int offset, int limit, int totalCount)
    {
        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if (limit < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(limit));
        }

        if (totalCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalCount));
        }

        Items = items;
        Offset = offset;
        Limit = limit;
        TotalCount = totalCount;
    }
    
    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}