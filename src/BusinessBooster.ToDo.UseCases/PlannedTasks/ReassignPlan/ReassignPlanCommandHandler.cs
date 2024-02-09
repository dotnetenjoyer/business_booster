using Microsoft.EntityFrameworkCore;
using MediatR;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using BusinessBooster.ToDo.UseCases.Plans;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.ReassignPlan;

/// <summary>
/// Handler for <see cref="ReassignPlanCommand"/>.
/// </summary>
public class ReassignPlanCommandHandler : IRequestHandler<ReassignPlanCommand>
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IDbContext dbContext;
    private readonly PlansService plansService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ReassignPlanCommandHandler(ILoggedUserAccessor loggedUserAccessor, 
        IDbContext dbContext, PlansService plansService)
    {
        this.loggedUserAccessor = loggedUserAccessor;
        this.dbContext = dbContext;
        this.plansService = plansService;
    }

    /// <inheritdoc />
    public async Task Handle(ReassignPlanCommand command, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        var task = await dbContext.Tasks
            .Where(x => x.Plan.UserId == loggedUserId && x.Id == command.TaskId)
            .FirstOrDefaultAsync(cancellationToken);

        if (task == null)
        {
            throw new NotFoundException("Task with the specified identifier were not found");
        }

        task.Plan = await plansService.FindPlanAsync(command.PlanId, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}