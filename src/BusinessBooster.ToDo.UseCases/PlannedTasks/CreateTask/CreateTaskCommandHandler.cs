using AutoMapper;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.UseCases.Plans;
using MediatR;
using TaskStatus = BusinessBooster.ToDo.Domain.Entities.TaskStatus;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.CreateTask;

/// <summary>
/// Handler for <see cref="CreateTaskCommand"/>.
/// </summary>
internal class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, IdDto<int>>
{
    private readonly IDbContext dbContext;
    private readonly IMapper mapper;
    private readonly PlanService planService;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateTaskCommandHandler(IDbContext dbContext, IMapper mapper, PlanService planService)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.planService = planService;
    }

    /// <inheritdoc />
    public async Task<IdDto<int>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        await planService.FindPlanAsync(command.PlanId, cancellationToken);
        
        var task = mapper.Map<PlannedTask>(command);
        var statusRecord = new TaskStatusRecord(task, TaskStatus.Pending);
        task.TaskStatusRecords.Add(statusRecord);
        dbContext.Tasks.Add(task);

        await dbContext.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}