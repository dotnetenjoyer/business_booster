using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using BusinessBooster.ToDo.UseCases;
using BusinessBooster.ToDo.UseCases.Plans.GetPlan;
using BusinessBooster.ToDo.UseCases.Plans.RemovePlan;
using BusinessBooster.ToDo.UseCases.Plans.SavePlan;

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
    public Task<PlanDto> GetPlanAsync(int id, CancellationToken cancellationToken)
        => mediator.Send(new GetPlanQuery(id), cancellationToken); 

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