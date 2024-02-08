using MediatR;
using BusinessBooster.ToDo.UseCases.Common.Pagination;
using BusinessBooster.ToDo.UseCases.Plans.Dtos;

namespace BusinessBooster.ToDo.UseCases.Plans.SearchPlans;

/// <summary>
/// Search for plans.
/// </summary>
public class SearchPlansQuery : PaginationSettings, IRequest<PagedListMetadataDto<PlanDto>>
{
    /// <summary>
    /// Allows filtering plans by name (searches plans that contains the filter string).
    /// </summary>
    public string? Filter { get; init; }
}