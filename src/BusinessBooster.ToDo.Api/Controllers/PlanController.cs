using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using BusinessBooster.ToDo.UseCases;
using BusinessBooster.ToDo.UseCases.Common.Pagination;
using BusinessBooster.ToDo.UseCases.Plans.Dtos;
using BusinessBooster.ToDo.UseCases.Plans.GetPlan;
using BusinessBooster.ToDo.UseCases.Plans.RemovePlan;
using BusinessBooster.ToDo.UseCases.Plans.SavePlan;
using BusinessBooster.ToDo.UseCases.Plans.SearchPlans;

namespace BusinessBooster.ToDo.Api.Controllers;

/// <summary>
/// Contains api methods to manage plans.
/// </summary>
[ApiController]
[Authorize]
[Route("/api/plan")]
public class PlanController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public PlanController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Get plan by id.
    /// </summary>
    [HttpGet("{id}")]
    public Task<DetailedPlanDto> GetPlanAsync(int id, CancellationToken cancellationToken)
        => mediator.Send(new GetPlanQuery(id), cancellationToken);

    /// <summary>
    /// Search for plans.
    /// </summary>
    [HttpPost("search")]
    public Task<PagedListMetadataDto<PlanDto>> SearchPlansAsync(SearchPlansQuery query, CancellationToken cancellationToken)
        => mediator.Send(query, cancellationToken); 
    
    /// <summary>
    /// Saves a plan. 
    /// </summary>
    [HttpPost]
    public Task<IdDto<int>> SavePlanAsync(SavePlanCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Removes a plan.
    /// </summary>
    [HttpDelete("{id}")]
    public Task RemovePlanAsync(int id, CancellationToken cancellationToken)
        => mediator.Send(new RemovePlanCommand(id), cancellationToken);
}