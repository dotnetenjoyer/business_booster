using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;

namespace BusinessBooster.ToDo.UseCases.Plans.SavePlan;

/// <summary>
/// Handler for <see cref="SavePlanCommand"/>. 
/// </summary>
internal class SavePlanCommandHandler : IRequestHandler<SavePlanCommand, IdDto<int>>
{
    private readonly IDbContext dbContext;
    private readonly IMapper mapper;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public SavePlanCommandHandler(IDbContext dbContext, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    public async Task<IdDto<int>> Handle(SavePlanCommand command, CancellationToken cancellationToken)
    {
        if (command.Id is null or < 1)
        {
            return await CreatePlanAsync(command, cancellationToken);
        }

        return await SavePlanAsync(command, cancellationToken);
    }
    
    private async Task<IdDto<int>> CreatePlanAsync(SavePlanCommand command, CancellationToken cancellationToken)
    {
        var plan = mapper.Map<Plan>(command);
        plan.UserId = loggedUserAccessor.GetCurrentUserId();

        dbContext.Plans.Add(plan);
        await dbContext.SaveChangesAsync(cancellationToken);

        return plan.Id;
    }

    private async Task<IdDto<int>> SavePlanAsync(SavePlanCommand command, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();

        var plan = await dbContext.Plans
            .Where(x => x.Id == command.Id && x.UserId == loggedUserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (plan == null)
        {
            throw new NotFoundException("No plan with the specified identifier was found.");
        }

        plan.Name = command.Name;
        plan.Description = command.Description;

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return plan.Id;
    }
}