using BusinessBooster.ToDo.Domain.Entities;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace BusinessBooster.ToDo.UseCases.Plans.Dtos;

/// <summary>
/// Plan DTO.
/// </summary>
public class PlanDto
{
    /// <inheritdoc cref="Plan.Id"/>>
    public int Id { get; set; }
    
    /// <inheritdoc cref="Plan.Name"/>>
    public string Name { get; set; }
    
    /// <inheritdoc cref="Plan.Description"/>>
    public string Description { get; set; }
}

public class DetailedPlanDto
{
    /// <inheritdoc cref="Plan.Id"/>>
    public int Id { get; set; }
    
    /// <inheritdoc cref="Plan.Name"/>>
    public string Name { get; set; }
    
    /// <inheritdoc cref="Plan.Description"/>>
    public string Description { get; set; }

    /// <inheritdoc cref="Plan.Tasks"/>>
    public ICollection<TaskDto> Tasks { get; set; }
}

public class TaskDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }

    public TaskStatus Status { get; set; }
}