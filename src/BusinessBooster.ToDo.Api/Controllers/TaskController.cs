using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BusinessBooster.ToDo.UseCases;
using BusinessBooster.ToDo.UseCases.PlannedTasks.ReassignPlan;
using BusinessBooster.ToDo.UseCases.PlannedTasks.RemoveTask;
using BusinessBooster.ToDo.UseCases.PlannedTasks.SaveTask;

namespace BusinessBooster.ToDo.Api.Controllers;

/// <summary>
/// Contains api methods to manage planned tasks.
/// </summary>
[ApiController]
[Authorize]
[Route("/api/task")]
public class TaskController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public TaskController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Saves a task.
    /// </summary>
    [HttpPost]
    public Task<IdDto<int>> SaveAsync(SaveTaskCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Reassign task plan.
    /// </summary>
    [HttpPost("reassign")]
    public Task ReassignPlanAsync(ReassignPlanCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);
    
    /// <summary>
    /// Removes a task.
    /// </summary>
    [HttpDelete("{id}")]
    public Task RemoveAsync(int id, CancellationToken cancellationToken)
        => mediator.Send(new RemoveTaskCommand(id), cancellationToken);
}