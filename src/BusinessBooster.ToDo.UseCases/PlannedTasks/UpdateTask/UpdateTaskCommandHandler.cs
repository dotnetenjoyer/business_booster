using Microsoft.EntityFrameworkCore;
using MediatR;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.UpdateTask;

/// <summary>
/// Handler for <see cref="UpdateTaskCommand"/>.
/// </summary>
internal class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, IdDto<int>>
{
    private readonly IDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateTaskCommandHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    public async Task<IdDto<int>> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        var task = await dbContext.Tasks
            .Include(x => x.TaskStatusRecords)
            .Where(x => x.Id == command.Id && x.Plan.UserId == loggedUserId)
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