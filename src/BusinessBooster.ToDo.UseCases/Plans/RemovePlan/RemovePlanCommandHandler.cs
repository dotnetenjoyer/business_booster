using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.UseCases.Plans.RemovePlan;

/// <summary>
/// Handler for <see cref="RemovePlanCommand"/>.
/// </summary>
internal class RemovePlanCommandHandler : IRequestHandler<RemovePlanCommand>
{
    private readonly IDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemovePlanCommandHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inhertidoc />
    public async Task Handle(RemovePlanCommand command, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();

        var plan = await dbContext.Plans
            .Where(x => x.Id == command.Id && x.UserId == loggedUserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (plan != null)
        {
            plan.RemovedAt = DateTime.Now;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}