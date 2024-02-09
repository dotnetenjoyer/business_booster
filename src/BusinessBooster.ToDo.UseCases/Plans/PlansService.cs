using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.UseCases.Plans;

/// <summary>
/// Contains common methods to mange plans.
/// </summary>
public class PlansService
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public PlansService(ILoggedUserAccessor loggedUserAccessor, IDbContext dbContext)
    {
        this.loggedUserAccessor = loggedUserAccessor;
        this.dbContext = dbContext;
    }
    
    /// <summary>
    /// Ensures that plan with the specified id exists.
    /// </summary>
    /// <param name="planId">Plan id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task<Plan> FindPlanAsync(int planId, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        var plan = await dbContext.Plans
            .Include(x => x.Tasks).ThenInclude(x => x.TaskStatusRecords)
            .Where(x => x.Id == planId && x.UserId == loggedUserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (plan == null)
        {
            throw new NotFoundException("Plan with the specified identifier were not found");
        }

        return plan;
    }
}