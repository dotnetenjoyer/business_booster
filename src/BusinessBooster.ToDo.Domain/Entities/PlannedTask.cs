namespace BusinessBooster.ToDo.Domain.Entities;

/// <summary>
/// Planned task entity.
/// </summary>
public class PlannedTask
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Created at date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Plan Id.
    /// </summary>
    public int PlanId { get; set; }
    
    /// <summary>
    /// Plan.
    /// </summary>
    public Plan Plan { get; set; }

    /// <summary>
    /// History of task statuses.
    /// </summary>
    public ICollection<TaskStatusRecord> StatusHistory { get; set; }

    /// <summary>
    /// Task status.
    /// </summary>
    public TaskStatus Status => StatusHistory.LastOrDefault()?.Status ?? TaskStatus.Pending;
}