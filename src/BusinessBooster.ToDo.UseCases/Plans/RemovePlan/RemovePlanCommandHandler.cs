using MediatR;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;

namespace BusinessBooster.ToDo.UseCases.Plans.RemovePlan;

/// <summary>
/// Handler for <see cref="RemovePlanCommand"/>.
/// </summary>
internal class RemovePlanCommandHandler : IRequestHandler<RemovePlanCommand>
{
    private readonly IDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly PlanService _planService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemovePlanCommandHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor, PlanService planService)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
        this._planService = planService;
    }

    /// <inhertidoc />
    public async Task Handle(RemovePlanCommand command, CancellationToken cancellationToken)
    {
        var plan = await _planService.FindPlanAsync(command.Id, cancellationToken);
        if (plan.Tasks.Count > 0)
        {
            throw new DomainException("You cannot delete a plan with assigned tasks.");
        }

        dbContext.Plans.Remove(plan);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}