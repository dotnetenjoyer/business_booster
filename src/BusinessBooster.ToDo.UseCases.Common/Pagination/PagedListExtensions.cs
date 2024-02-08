namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// Contains extension methods to <see cref="PagedList{T}"/>
/// </summary>
public static class PagedListExtensions
{
    /// <summary>
    /// Creates <see cref="PagedListMetadataDto{T}"/> from <see cref="PagedList{T}"/>.
    /// </summary>
    /// <param name="pagedList">Paged list.</param>
    /// <returns>Paged list metadata object.</returns>
    public static PagedListMetadataDto<T> ToMetadataObject<T>(this PagedList<T> pagedList) => new(pagedList);
}