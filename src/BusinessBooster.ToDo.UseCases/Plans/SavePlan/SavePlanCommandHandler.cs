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
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IDbContext dbContext;
    private readonly IMapper mapper;
    private readonly PlanService _planService;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public SavePlanCommandHandler(IDbContext dbContext, IMapper mapper, ILoggedUserAccessor loggedUserAccessor, 
        PlanService planService)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
        this._planService = planService;
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
        var plan = await _planService.FindPlanAsync(command.Id.Value, cancellationToken);
        plan.Name = command.Name;
        plan.Description = command.Description;
        await dbContext.SaveChangesAsync(cancellationToken);

        return plan.Id;
    }
}