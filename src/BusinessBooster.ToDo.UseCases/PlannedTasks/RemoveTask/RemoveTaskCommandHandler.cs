using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.RemoveTask;

/// <summary>
/// Handler for <see cref="RemoveTaskCommand"/>.
/// </summary>
public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand>
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemoveTaskCommandHandler(ILoggedUserAccessor loggedUserAccessor, IDbContext dbContext)
    {
        this.loggedUserAccessor = loggedUserAccessor;
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task Handle(RemoveTaskCommand command, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        var task = await dbContext.Tasks
            .Where(x => x.Plan.UserId == loggedUserId && x.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (task != null)
        {
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}