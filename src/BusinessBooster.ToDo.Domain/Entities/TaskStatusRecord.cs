namespace BusinessBooster.ToDo.Domain.Entities;

/// <summary>
/// Entity store task status history.
/// </summary>
public class TaskStatusRecord
{
    /// <summary>
    /// Task status.
    /// </summary>
    public TaskStatus Status { get; set; }
    
    /// <summary>
    /// Related task id.
    /// </summary>
    public int TaskId { get; set; }
    
    /// <summary>
    /// Related task.
    /// </summary>
    public PlannedTask Task { get; set; }
    
    /// <summary>
    /// Created at date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}