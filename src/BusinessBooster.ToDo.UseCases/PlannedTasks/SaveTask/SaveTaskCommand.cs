using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using BusinessBooster.ToDo.Domain.Entities;
using TaskStatus = BusinessBooster.ToDo.Domain.Entities.TaskStatus;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.SaveTask;

/// <summary>
/// Command to save a task.
/// </summary>
public class SaveTaskCommand : IRequest<IdDto<int>>
{
    /// <inheritdoc cref="PlannedTask.Id"/>>
    public int? Id { get; init; }
    
    /// <inheritdoc cref="PlannedTask.Name"/>>
    [Required]
    public string Name { get; set; }
    
    /// <inheritdoc cref="PlannedTask.Description"/>>
    public string? Description { get; set; }
    
    /// <inheritdoc cref="PlannedTask.PlanId"/>>
    [Range(1, int.MaxValue)]
    [DisplayName("Plan Id")]
    public int PlanId { get; set; }

    /// <inheritdoc cref="PlannedTask.Status"/>>
    public TaskStatus Status { get; set; }
}