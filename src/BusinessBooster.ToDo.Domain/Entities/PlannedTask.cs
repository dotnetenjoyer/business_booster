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
    /// Task status records.
    /// </summary>e
    public List<TaskStatusRecord> TaskStatusRecords { get; set; } = new();

    /// <summary>
    /// Task status.
    /// </summary>
    public TaskStatus Status
    {
        get
        {
            if (TaskStatusRecords == null)
            {
                throw new InvalidOperationException("To get the status of a task, you need to upload the task status records.");
            }

            return TaskStatusRecords.LastOrDefault()?.Status ?? TaskStatus.Pending;
        }
    }
}