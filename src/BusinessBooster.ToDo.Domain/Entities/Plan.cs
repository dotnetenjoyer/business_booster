namespace BusinessBooster.ToDo.Domain.Entities;

/// <summary>
/// Plan entity.
/// </summary>
public class Plan
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Description.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Created at date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// User id.
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// User.
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Collection of planned tasks.
    /// </summary>
    public ICollection<PlannedTask> Tasks { get; set; }
}