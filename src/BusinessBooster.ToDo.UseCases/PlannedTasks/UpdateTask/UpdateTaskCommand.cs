using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using BusinessBooster.ToDo.Domain.Entities;
using TaskStatus = BusinessBooster.ToDo.Domain.Entities.TaskStatus;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.UpdateTask;

/// <summary>
/// Command to update a task.
/// </summary>
public class UpdateTaskCommand : IRequest<IdDto<int>>
{
    /// <inheritdoc cref="PlannedTask.Id"/>>
    [Range(1, int.MaxValue)]
    public int Id { get; init; }
    
    /// <inheritdoc cref="PlannedTask.Name"/>>
    [Required]
    public string Name { get; set; }
    
    /// <inheritdoc cref="PlannedTask.Description"/>>
    public string? Description { get; set; }

    /// <inheritdoc cref="PlannedTask.Status"/>>
    public TaskStatus Status { get; set; }
}