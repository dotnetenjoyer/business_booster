namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// Contains metadata for <see cref="PagedList{T}"/>.
/// </summary>
public class PagedListMetadataDto<T>
{
    private readonly PagedList<T> pagedList;

    /// <inheritdoc cref="PagedList{T}.Page"/>>
    public int Page => pagedList.Page;

    /// <inheritdoc cref="PagedList{T}.PageSize"/>>
    public int PageSize => pagedList.PageSize;

    /// <inheritdoc cref="PagedList{T}.TotalPages"/>>
    public int TotalPages => pagedList.TotalPages;

    /// <inheritdoc cref="PagedList{T}.Items"/>>
    public IEnumerable<T> Items => pagedList.Items;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public PagedListMetadataDto(PagedList<T> pagedList)
    {
        this.pagedList = pagedList;
    }
}