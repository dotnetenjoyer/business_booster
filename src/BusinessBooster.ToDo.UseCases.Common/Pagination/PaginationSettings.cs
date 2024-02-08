using System.ComponentModel.DataAnnotations;

namespace BusinessBooster.ToDo.UseCases.Common.Pagination;

/// <summary>
/// The class that contains settings for object pagination.
/// </summary>
public class PaginationSettings
{
    /// <summary>
    /// Number of current page.
    /// </summary>
    [Range(1, int.MaxValue)] 
    public int Page { get; set; } = PaginationHelper.FirstPage;

    /// <summary>
    /// Required page size.
    /// </summary>
    [Range(1, 1000)]
    public int PageSize { get; set; } = 100;
}