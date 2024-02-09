using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessBooster.ToDo.Domain.Entities;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.CreateTask;

/// <summary>
/// Command to create task.
/// </summary>
public class CreateTaskCommand : IRequest<IdDto<int>>
{
    /// <inheritdoc cref="PlannedTask.Name"/>>
    [Required]
    public string Name { get; set; }
    
    /// <inheritdoc cref="PlannedTask.Description"/>>
    public string? Description { get; set; }
    
    /// <inheritdoc cref="PlannedTask.PlanId"/>>
    [Range(1, int.MaxValue)]
    [DisplayName("Plan Id")]
    public int PlanId { get; set; }
}