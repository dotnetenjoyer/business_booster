using MediatR;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.RemoveTask;

/// <summary>
/// Command to remove task.
/// </summary>
/// <param name="Id">Task id.</param>
public record RemoveTaskCommand(int Id) : IRequest;