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
    private readonly PlansService planService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemovePlanCommandHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor, PlansService planService)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
        this.planService = planService;
    }

    /// <inhertidoc />
    public async Task Handle(RemovePlanCommand command, CancellationToken cancellationToken)
    {
        var plan = await planService.FindPlanAsync(command.Id, cancellationToken);
        if (plan.Tasks.Count > 0)
        {
            throw new DomainException("You cannot delete a plan with assigned tasks.");
        }

        dbContext.Plans.Remove(plan);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}