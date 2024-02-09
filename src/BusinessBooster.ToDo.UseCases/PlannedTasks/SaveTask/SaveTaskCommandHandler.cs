using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using BusinessBooster.ToDo.UseCases.Plans;
using TaskStatus = BusinessBooster.ToDo.Domain.Entities.TaskStatus;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.SaveTask;

/// <summary>
/// Handler for <see cref="SaveTaskCommand"/>.
/// </summary>
internal class SaveTaskCommandHandler : IRequestHandler<SaveTaskCommand, IdDto<int>>
{
    private readonly IDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;
    private readonly PlansService plansService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SaveTaskCommandHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor, IMapper mapper, 
        PlansService plansService)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
        this.mapper = mapper;
        this.plansService = plansService;
    }

    /// <inheritdoc />
    public Task<IdDto<int>> Handle(SaveTaskCommand command, CancellationToken cancellationToken)
    {
        if (command.Id is > 0)
        {
            return SaveAsync(command, cancellationToken);
        }

        return CreateAsync(command, cancellationToken);
    }

    private async Task<IdDto<int>> CreateAsync(SaveTaskCommand command, CancellationToken cancellationToken)
    {
        await plansService.FindPlanAsync(command.PlanId, cancellationToken);
        
        var task = mapper.Map<PlannedTask>(command);
        var statusRecord = new TaskStatusRecord(task, TaskStatus.Pending);
        task.TaskStatusRecords.Add(statusRecord);
        dbContext.Tasks.Add(task);

        await dbContext.SaveChangesAsync(cancellationToken);
        return task.Id;
    }

    private async Task<IdDto<int>> SaveAsync(SaveTaskCommand command, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        var task = await dbContext.Tasks
            .Include(x => x.TaskStatusRecords)
            .Where(x => x.PlanId == command.PlanId && x.Plan.UserId == loggedUserId)
            .Where(x => x.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (task == null)
        {
            throw new NotFoundException("Task with the specified identifier were not found");
        }

        task.Name = command.Name;
        task.Description = command.Description;

        if (task.Status != command.Status)
        {
            var statusRecord = new TaskStatusRecord(task, command.Status);
            task.TaskStatusRecords.Add(statusRecord);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return command.Id;  
    }
}