using BusinessBooster.ToDo.Domain.Entities;

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